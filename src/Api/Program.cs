// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


// For SwaggerGen extension methods

using System.Text;

using Domain.Entities;

using Infrastructure.Data;
using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        // Register both DbContext and DbContextFactory for DI
        _ = builder.Services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        _ = builder.Services.AddAuthorization();

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
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    /// <summary>
    /// Configures JWT Bearer authentication for the application.
    /// </summary>
    /// <param name="builder">A web application builder instance.</param>
    private static void ConfigureJwtAuthentication(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
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
                Console.WriteLine("[DEBUG] Checking for placeholder: " + placeholder);
                if (connStr.Contains(placeholder))
                {
                    Console.WriteLine($"[DEBUG] Found placeholder {placeholder} in connection string. Attempting to replace it with environment variable value.");
                    connStr = connStr.Replace(placeholder, Environment.GetEnvironmentVariable(placeholder.Trim('{', '}'))
                        ?? throw new InvalidOperationException($"Environment variable for {placeholder} not found in configuration."));
                }
            }

            builder.Configuration["ConnectionStrings:AppDbContext"] = connStr;

            // After building connStr in ConfigureDbContext
            string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string safeConnStr = connStr;
            if (!string.IsNullOrEmpty(dbPassword))
            {
                safeConnStr = connStr.Replace(dbPassword, "****");
            }
            Console.WriteLine($"\n\n[DEBUG] Final DB connection string: {safeConnStr}\n\n");
            Console.WriteLine("[DEBUG] MYSQL_HOST at startup: " + Environment.GetEnvironmentVariable("MYSQL_HOST"));
            return connStr;
        }
        throw new InvalidOperationException("Connection string for AppDbContext not found in environment variables.");
    }

    /// <summary>
    /// Adds the Identity API endpoints with custom registration authorization.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    //private static IEndpointConventionBuilder AddEndpointIdentityApi(WebApplication app)
    //{
    //    // Suppress the default /register endpoint so only custom registration is allowed
    //    IEndpointConventionBuilder identityApis = app.MapGroup("/Account").MapIdentityApi<User>();

    //    _ = identityApis.AddEndpointFilter(async (context, next) =>
    //    {
    //        if (context.HttpContext.Request.Path.StartsWithSegments("/register", StringComparison.OrdinalIgnoreCase))
    //        {
    //            ClaimsPrincipal user = context.HttpContext.User;

    //            // Check if the user is authenticated and has the specific claim
    //            if (!user.Identity?.IsAuthenticated == true ||
    //                !user.HasClaim(c => c.Type == "CanManageUsers"))
    //            {
    //                return Results.Forbid();
    //            }
    //        }
    //        return await next(context);
    //    });
    //    return identityApis;
    //}
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
