using AutoMapper.QueryableExtensions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Cars;

/// <summary>
/// Обработчик запроса списка автомобиля
/// </summary>
public class GetCarListQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListQuery, CarList>
{
    public async Task<CarList> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        var entityEnumer = await repository.GetAllAsync(cancellationToken);
        
        var carQuery = entityEnumer
            .AsQueryable()
            .ProjectTo<CarLookupDto>(mapper.ConfigurationProvider) // Расширение из AutoMapper
            .ToList();

        return new CarList { Cars = carQuery };
    }
}