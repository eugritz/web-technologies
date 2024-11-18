using Microsoft.EntityFrameworkCore;
using WebTechnologies.Core;

namespace WebTechnologies.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dealer>()
                .HasMany(e => e.Cars)
                .WithMany("Dealers")
                .UsingEntity("DealerCars");
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
    }
}
