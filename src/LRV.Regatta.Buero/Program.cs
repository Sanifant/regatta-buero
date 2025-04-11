
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

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DatabaseConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DatabaseConnection"))
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
