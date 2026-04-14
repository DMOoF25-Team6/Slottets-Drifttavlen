// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


// For SwaggerGen extension methods

using Domain.Entities;

using Core;

using Infrastructure;

using Infrastructure.Data;
using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
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
        string connectionString = DbContextConfiguration(builder, conf);
        // Register both DbContext and DbContextFactory for DI
        _ = builder.Services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        //_ = builder.Services.AddDbContextFactory<AppDbContext>();

        _ = builder.Services.AddAuthorization();

        _ = builder.Services.AddInfrastructureData();
        _ = builder.Services.AddInfrastructure();
        _ = builder.Services.AddCore();

        _ = builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredLength = 7;
        })
            .AddEntityFrameworkStores<AppDbContext>();
        //.AddDefaultTokenProviders();

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

        _ = app.MapControllers();

        app.Run();
    }

    public static string DbContextConfiguration(WebApplicationBuilder builder, ConfigurationManager conf)
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

public class DummyEmailSenderForUser : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        // Log or ignore in development
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        // Log or ignore in development
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        // Log or ignore in development
        return Task.CompletedTask;
    }
}
