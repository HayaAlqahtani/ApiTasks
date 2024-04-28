using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
