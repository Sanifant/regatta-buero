
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace LRV.Regatta.Buero
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("origin",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"; // Default-Port
            var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "regatta_database";
            var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "regatta";
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "regatta";

            var connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";

            Console.WriteLine($"Connecting to DB {dbHost}:{dbPort} using \"{connectionString}\"");

            MariaDbServerVersion serverVersion = new MariaDbServerVersion(new Version(11, 4, 5));
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(
                    connectionString,
                    serverVersion
                )
                .LogTo(Console.WriteLine, LogLevel.Information)
            );

            builder.Services.AddScoped<IFinishService, MysqlDataService>();
            builder.Services.AddScoped<IRegistrationService, MysqlDataService>();
            builder.Services.AddScoped<ILogService, MysqlDataService>();

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Regatta Software API",
                        Version = "v1"
                    }
                 );

                c.IgnoreObsoleteActions();
                c.IncludeXmlComments(Assembly.GetExecutingAssembly());

                // Füge die API Key-Definition hinzu
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Fügen Sie hier den API Key ein"
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new List<string>()
                    }
                };

                c.AddSecurityRequirement(securityRequirement);
            });

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Key"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                };
            });

            builder.Services.AddHealthChecks()
                .AddRedis(
                    redisConnectionString: builder.Configuration.GetConnectionString("Redis"),
                    name: "redis",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "ready" })
                .AddMySql(
                    connectionString: connectionString,
                    name: "mysql",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "ready" });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("origin");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description
                        })
                    });

                    await context.Response.WriteAsync(result);
                }
            });


            using IServiceScope scope = app.Services.CreateScope();

            ApplyMigration<DatabaseContext>(scope);

            app.Run();
        }

        public static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
        {
            using TDbContext context = scope.ServiceProvider
                .GetRequiredService<TDbContext>();

            context.Database.Migrate();
        }
    }
}
