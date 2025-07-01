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
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUnitOfWorkCarApi, UnitOfWork<CarApiDbContext>>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}