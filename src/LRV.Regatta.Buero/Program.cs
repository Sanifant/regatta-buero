
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

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
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            builder.Services.AddScoped<IFinishService, MysqlDataService>();
            builder.Services.AddScoped<IRegistrationService, MysqlDataService>();

            builder.Services.AddControllers();
            
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("origin");

            app.UseAuthorization();
            app.MapControllers();

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
