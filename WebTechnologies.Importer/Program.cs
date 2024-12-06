using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebTechnologies.Data;

namespace WebTechnologies.Importer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<DataContext>();
            if (builder["Database"] == "SqlServer")
            {
                options.UseSqlServer(builder.GetConnectionString("SqlServer")
                    ?? builder.GetConnectionString("Default"));
            }
            else if (builder["Database"] == "PostreSQL")
            {
                options.UseNpgsql(builder.GetConnectionString("PostreSQL")
                    ?? builder.GetConnectionString("Default"));
            }

            using (var context = new DataContext(options.Options))
            {
                new DbInitializer(context, new SimpleLogger()).ImportJson();
            }
        }
    }
}
