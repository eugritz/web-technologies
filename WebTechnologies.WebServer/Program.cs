using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using WebTechnologies.Data;
using WebTechnologies.Data.Decorators;

namespace WebTechnologies.WebServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options =>
            {
                if (builder.Configuration["Database"] == "SqlServer")
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")
                        ?? builder.Configuration.GetConnectionString("Default"));
                }
                else if (builder.Configuration["Database"] == "PostreSQL")
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("PostreSQL")
                        ?? builder.Configuration.GetConnectionString("Default"));
                }
            });

            {
                var factory = new ConnectionFactory
                {
                    HostName = builder.Configuration["RabbitMQ_HostName"],
                };
                builder.Services.AddScoped((provider) => factory.CreateConnection());
            }

            // Register repositores
            builder.Services.AddScoped<CarRepository>();
            builder.Services.AddScoped<ICarRepository, RabbitMQCarRepository>();
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
