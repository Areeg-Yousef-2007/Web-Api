using Microsoft.EntityFrameworkCore;
using WeatherApp.Entity;

namespace WeatherApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

       public  DbSet<City> Cities { get; set; }
      public  DbSet<Weather> Weathers { get; set; }
       public DbSet<Forecast> Forecasts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasOne(c => c.Weather)
                .WithOne(w => w.City)
                .HasForeignKey<Weather>(w => w.CityId);
        }
    }
}
