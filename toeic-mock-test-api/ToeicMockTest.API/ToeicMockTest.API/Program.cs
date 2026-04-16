
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToeicMockTest.Infrastructure.Persistence;
using ToeicMockTest.Infrastructure;
using ToeicMockTest.Application; 
namespace ToeicMockTest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVite",
                    policy => policy.WithOrigins("http://localhost:5173") 
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });
            var app = builder.Build();
            app.UseCors("AllowVite");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
