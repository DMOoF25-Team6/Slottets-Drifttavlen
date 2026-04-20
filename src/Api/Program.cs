// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


// For SwaggerGen extension methods

using Api.Helpers;

using Domain.Entities;

using Infrastructure.Data;
using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        // Load environment variables from .env file
        _ = DotNetEnv.Env.Load(AppContext.BaseDirectory);

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
        string connectionString = ConfigureDbContext(builder, conf);
        // Register both DbContext and DbContextFactory for DI
        _ = builder.Services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        //_ = builder.Services.AddDbContextFactory<AppDbContext>();

        _ = builder.Services.AddAuthorization();

        _ = builder.Services.AddInfrastructureData();
        //_ = builder.Services.AddInfrastructure();
        //_ = builder.Services.AddCore();

        ConfigureIdentity(builder);
        ConfigureJwtAuthentication(builder);

        // Add services to the container.
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddSwaggerGen();

        _ = builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

        // Dummy email sender for Identity (required by MapIdentityApi)
        _ = builder.Services.AddSingleton<IEmailSender<User>, DummyEmailSenderForUser>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseCors("EnableCORS");

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        // Add Identity API endpoints with custom registration authorization
        _ = app.AddEndpointIdentityApi();

        _ = app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Configures ASP.NET Core Identity for the application.
    /// </summary>
    /// <param name="builder">A web application builder instance.</param>
    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddIdentityCore<User>(opt =>
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
        _ = builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // IssuerSigningKey should be set here for production use

                    ValidIssuer = builder.Configuration["TokenValidationParameters:ValidIssuer"],
                    ValidAudience = builder.Configuration["TokenValidationParameters:ValidAudience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["TokenValidationParameters:IssuerSigningKey"] ?? throw new InvalidOperationException("IssuerSigningKey not found in configuration."))
                    )
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
            string dbUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new InvalidOperationException("MYSQL_USER environment variable not found.");
            string dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new InvalidOperationException("MYSQL_PASSWORD environment variable not found.");
            string dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new InvalidOperationException("MYSQL_HOST environment variable not found.");
            string dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new InvalidOperationException("MYSQL_DATABASE environment variable not found.");
            connStr = connStr.Replace("{DB_USER}", dbUser)
                             .Replace("{DB_PASSWORD}", dbPass)
                             .Replace("{DB_HOST}", dbHost)
                             .Replace("{DB_NAME}", dbName);
            builder.Configuration["ConnectionStrings:AppDbContext"] = connStr;
            return connStr;
        }
        throw new InvalidOperationException("Connection string for AppDbContext not found in environment variables.");
    }
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
