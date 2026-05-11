// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


// For SwaggerGen extension methods

using System.Diagnostics;
using System.Security.Claims;
using System.Text;

using Domain.Entities;

using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using MySqlConnector;
namespace Api;


/// <summary>
/// Entry point and configuration for the API application.
/// </summary>
/// <remarks>
/// Configures services, authentication, authorization, and middleware for the API.
/// </remarks>
public class Program
{
    /// <summary>
    /// Initializes and runs the API application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    public static void Main(string[] args)
    {

#if DEBUG
        // Load environment variables from .env file
        // Docker load .env and make overwriting, why this only can be loaded in debug mode
        _ = DotNetEnv.Env.Load(AppContext.BaseDirectory);
#endif

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        _ = builder.Services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", policy =>
            {
                _ = policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Replace the connection string params with the one from the environment variable
        ConfigurationManager conf = builder.Configuration;
        string connectionString = ConfigureDbContext(builder, conf)
            ?? throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");
        try
        {
            using MySqlConnection testConnection = new(connectionString);
            testConnection.Open();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ERROR] Failed to connect to the database. Exception: " + ex.Message);
        }

        // The rest of the pipeline configuration remains unchanged
        // CreateAccountAsync both DbContext and DbContextFactory for DI
        _ = builder.Services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        _ = builder.Services.AddInfrastructure(builder.Configuration);
        _ = builder.Services.AddInfrastructureData();

        ConfigureIdentity(builder);
        ConfigureJwtAuthentication(builder);

        // Add services to the container.
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddSwaggerGen();

        //_ = builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

        // Dummy email sender for Identity (required by MapIdentityApi)
        _ = builder.Services.AddSingleton<IEmailSender<User>, DummyEmailSenderForUser>();

        WebApplication app = builder.Build();

        // Apply any pending migrations at startup
        //using IServiceScope scope = app.Services.CreateScope();
        //AppDbContext ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //if (ctx.DatabaseController.IsRelational())
        //{
        //    ctx.DatabaseController.Migrate();
        //}

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        });
        //}

        //_ = app.UseHttpsRedirection();

        _ = app.UseCors("EnableCORS");

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        app.UseMiddleware<Api.Middleware.CurrentUserMiddleware>();

        // Add Identity API endpoints with custom registration authorization
        //_ = AddEndpointIdentityApi(app);

        _ = app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Configures ASP.NET Core Identity for the application.
    /// </summary>
    /// <param name="builder">A web application builder instance.</param>
    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {
        // Use IdentityRole<Guid> to match User : IdentityUser<Guid>
        _ = builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredLength = 7;

            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 5;

            opt.User.RequireUniqueEmail = true;
        })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    /// <summary>
    /// Configures JWT Bearer authentication for the application.
    /// </summary>
    /// <param name="builder">A web application builder instance.</param>
    private static void ConfigureJwtAuthentication(WebApplicationBuilder builder)
    {
        // Set the default authentication and challenge scheme to JwtBearer.
        // AddIdentity overrides defaults to cookie-based auth; explicitly restoring JWT
        // here ensures [Authorize] uses Bearer tokens rather than cookie redirects.
        _ = builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                // Use configuration values for TokenValidationParameters validation
                string issuer = builder.Configuration["TokenValidationParameters:Issuer"] ?? throw new InvalidOperationException("TokenValidationParameters:Issuer not found in configuration.");
                string audience = builder.Configuration["TokenValidationParameters:Audience"] ?? throw new InvalidOperationException("TokenValidationParameters:Audience not found in configuration.");
                string key = builder.Configuration["TokenValidationParameters:IssuerSigningKey"] ?? throw new InvalidOperationException("TokenValidationParameters  :IssuerSigningKey not found in configuration.");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });
        _ = builder.Services.AddAuthorization(options =>
        {
            // Require one of the emitted role claims so the policy matches the JWT contents.
            // This preserves the intended authorization behavior without changing token issuance.
            options.AddPolicy("CanManageResidents", policy =>
                policy.RequireRole("admin", "superuser"));
        });
    }

    /// <summary>
    /// Configures the database context connection string using environment variables.
    /// </summary>
    /// <param name="builder">A web application builder instance.</param>
    /// <param name="conf">A configuration manager instance.</param>
    /// <returns>A connection string for the application's database context.</returns>
    /// <exception cref="InvalidOperationException">A required environment variable or connection string is missing.</exception>
    public static string ConfigureDbContext(WebApplicationBuilder builder, ConfigurationManager conf)
    {
        string? connStr = conf.GetConnectionString("AppDbContext");
        if (!string.IsNullOrEmpty(connStr))
        {
            // find all placeholders in the connection string and replace them with environment variable values
            string[] allPlaceholders = ["{MYSQL_HOST}", "{MYSQL_DATABASE}", "{MYSQL_USER}", "{MYSQL_PASSWORD}", "{MYSQL_PORT}"];
            foreach (string placeholder in allPlaceholders)
            {
                if (connStr.Contains(placeholder))
                {
                    connStr = connStr.Replace(placeholder, Environment.GetEnvironmentVariable(placeholder.Trim('{', '}'))
                        ?? throw new InvalidOperationException($"Environment variable for {placeholder} not found in configuration."));
                }
            }

            builder.Configuration["ConnectionStrings:AppDbContext"] = connStr;

            // After building connStr in ConfigureDbContext
            string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            //string safeConnStr = connStr;
            if (!string.IsNullOrEmpty(dbPassword))
            {
                _ = connStr.Replace(dbPassword, "****");
            }
            return connStr;
        }
        throw new InvalidOperationException("Connection string for AppDbContext not found in environment variables.");
    }


    /// <summary>
    /// Dummy email sender implementation for development and testing.
    /// </summary>
    /// <remarks>
    /// This class is used to satisfy the <see cref="IEmailSender{TUser}"/> dependency for Identity without sending real emails.
    /// </remarks>
    public class DummyEmailSenderForUser : IEmailSender<User>
    {
        /// <summary>
        /// Simulates sending a confirmation link email.
        /// </summary>
        /// <param name="user">A user entity.</param>
        /// <param name="email">An email address.</param>
        /// <param name="confirmationLink">A confirmation link.</param>
        /// <returns>A completed task.</returns>
        public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            // Log or ignore in development
            return Task.CompletedTask;
        }

        /// <summary>
        /// Simulates sending a password reset link email.
        /// </summary>
        /// <param name="user">A user entity.</param>
        /// <param name="email">An email address.</param>
        /// <param name="resetLink">A password reset link.</param>
        /// <returns>A completed task.</returns>
        public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            // Log or ignore in development
            return Task.CompletedTask;
        }

        /// <summary>
        /// Simulates sending a password reset code email.
        /// </summary>
        /// <param name="user">A user entity.</param>
        /// <param name="email">An email address.</param>
        /// <param name="resetCode">A password reset code.</param>
        /// <returns>A completed task.</returns>
        public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            // Log or ignore in development
            return Task.CompletedTask;
        }
    }

}
