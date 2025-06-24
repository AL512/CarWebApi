using CarWebApi.Behaviors;
using CarWebApi.Database;
using CarWebApi.Repositories;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi
{
    /// <summary>
    /// Внедряет зависимости
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавляет и регистрирует контекст БД
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="configuration">Конфиг приложения</param>
        /// <returns>Коллекция служб</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<CarApiDbContext>(options =>
            {
                // options.UseSqlServer(connectionString);
                options.UseSqlite(connectionString);
            });

            //Репозитории
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Валидация
            // Регистрируем ValidationBehavior, как реализацию IPipelineBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
