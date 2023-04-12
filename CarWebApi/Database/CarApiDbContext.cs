using CarWebApi.Database.EntityTypeConfigurations;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Database
{
    /// <summary>
    /// Реализация контекста БД
    /// </summary>
    public class CarApiDbContext : DbContext
    {
        /// <summary>
        /// Реализация контекста БД
        /// </summary>
        /// <param name="options">Параметры контекста</param>
        public CarApiDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Country> Countries { get; set; } = null;
        public DbSet<Brand> Brands { get; set; } = null;
        public DbSet<Car> Cars { get; set; } = null;

        /// <summary>
        /// Настройка схемы БД
        /// </summary>
        /// <param name="builder">Методы для конфигурации сущностей и отношений между ними</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
