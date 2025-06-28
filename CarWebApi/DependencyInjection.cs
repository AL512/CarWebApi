using CarWebApi.Behaviors;
using CarWebApi.Database;
using CarWebApi.Repositories;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<CarApiDbContext>(options =>
        {
            // options.UseSqlServer(connectionString);
            options.UseSqlite(connectionString);
        });

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<ICountryRepository, CountryRepository>();
        services.AddTransient<IBrandRepository, BrandRepository>();
        services.AddTransient<ICarRepository, CarRepository>();
        services.AddTransient<IUnitOfWork<CarApiDbContext>, UnitOfWork<CarApiDbContext>>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}