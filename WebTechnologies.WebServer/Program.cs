using Microsoft.EntityFrameworkCore;
using WebTechnologies.Data;

namespace WebTechnologies.WebServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebTechnologies;"));

            // Register repositores
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IDealerRepository, DealerRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();
            if (!builder.Environment.IsDevelopment())
                app.UseHttpsRedirection();

            app.MapControllers();
            app.Run();
        }
    }
}
