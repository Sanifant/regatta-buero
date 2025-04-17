
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

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

            var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";

            Console.WriteLine($"Connecting to DB {dbHost}:{dbPort} using {dbUser}-{dbPassword}");

            MariaDbServerVersion serverVersion = new MariaDbServerVersion(new Version(10, 5, 9));
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(
                    connectionString,
                    serverVersion
                )
            );

            // Add services to the container.
            builder.Services.AddScoped<IFinishService, MemoryFinishService>();
            builder.Services.AddScoped<IRegistrationService, MysqlRegistrationService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
