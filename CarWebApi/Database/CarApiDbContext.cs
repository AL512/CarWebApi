using CarWebApi.Database.EntityTypeConfigurations;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Database;

public class CarApiDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; } = null;
    public DbSet<Brand> Brands { get; set; } = null;
    public DbSet<Car> Cars { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CountryConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new CarConfiguration());

        base.OnModelCreating(builder);
    }
}