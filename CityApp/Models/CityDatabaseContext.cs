//using Microsoft.EntityFrameworkCore;
//using CityApp.Models;


using Microsoft.EntityFrameworkCore;
using CityApp.Models;

namespace CityApp.Models
{
    public class CityDatabaseContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8GF8PRF; Initial Catalog=CityDB; Integrated Security=True; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
        }
    }
}

