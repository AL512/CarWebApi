using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Queries.Cars;

/// <summary>
/// Обработчик запроса автомобиля
/// </summary>
public class GetCarDetailsQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarDetailsQuery, CarDetails>
{
    public async Task<CarDetails> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Car), request.Id);
        }

        return mapper.Map<CarDetails>(entity);
    }
}