using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Cars;

/// <summary>
/// Обработчик команды добавления автомобиля
/// </summary>
public class CreateCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCarCommand, Guid>
{
    public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Car entity = new Car
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BrandId = request.BrandId,
            Brand = request.Brand,
            Long = request.Long,
            Pow = request.Pow,
            Price = request.Price,
            CreationDate = DateTime.Now,
            ModifiedDate = null,
        };

        var repository = unitOfWork.GetRepository<Car>();
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        return entity.Id;
    }
}