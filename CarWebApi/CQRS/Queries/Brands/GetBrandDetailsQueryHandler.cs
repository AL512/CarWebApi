using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Queries.Brands;

/// <summary>
/// Обработчик запроса марки автомобиля
/// </summary>
public class GetBrandDetailsQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetBrandDetailsQuery, BrandDetails>
{
    public async Task<BrandDetails> Handle(GetBrandDetailsQuery request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Brand>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Brand), request.Id);
        }

        return mapper.Map<BrandDetails>(entity);
    }
}