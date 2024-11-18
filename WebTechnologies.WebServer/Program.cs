using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
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

            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                };
                builder.Services.AddScoped((provider) => factory.CreateConnection());
            }

            // Register repositores
            builder.Services.AddScoped<Data.CarRepository>();
            builder.Services.AddScoped<ICarRepository, RabbitMQ.CarRepository>();
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
