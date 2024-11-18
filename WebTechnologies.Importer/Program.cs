using Microsoft.EntityFrameworkCore;
using WebTechnologies.Data;

namespace WebTechnologies.Importer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebTechnologies;")
                .Options;

            using (var context = new DataContext(options))
            {
                new DbInitializer(context, new SimpleLogger()).ImportJson();
            }
        }
    }
}
