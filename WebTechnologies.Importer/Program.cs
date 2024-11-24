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

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(
                    $"Data Source={builder["SqlServer_DataSource"]};" +
                    $"Initial Catalog={builder["SqlServer_InitialCatalog"]};")
                .Options;

            using (var context = new DataContext(options))
            {
                new DbInitializer(context, new SimpleLogger()).ImportJson();
            }
        }
    }
}
