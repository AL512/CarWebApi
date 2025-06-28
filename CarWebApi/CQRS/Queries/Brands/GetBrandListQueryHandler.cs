using AutoMapper.QueryableExtensions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Brands;

/// <summary>
/// Обработчик запроса списка марок автомобиля
/// </summary>
public class GetBrandListQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetBrandListQuery, BrandList>
{
    public async Task<BrandList> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Brand>();
        var entityEnumer = await repository.GetAllAsync(cancellationToken);
        
        var brandQuery = await entityEnumer
            .AsQueryable()
            .ProjectTo<BrandLookupDto>(mapper.ConfigurationProvider) // Расширение из AutoMapper
            .ToListAsync(cancellationToken);

        return new BrandList { Brands = brandQuery };
    }
}