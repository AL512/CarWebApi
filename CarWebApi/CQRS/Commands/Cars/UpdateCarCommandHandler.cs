using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Cars;

/// <summary>
/// Обработчик команды изменения автомобиля
/// </summary>
public class UpdateCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCarCommand>
{
    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity != null)
        {
            try
            {
                entity.Name = request.Name;
                entity.BrandId = request.BrandId;
                entity.Brand = request.Brand;
                entity.Pow = request.Pow;
                entity.Long = request.Long;
                entity.Price = request.Price;
                entity.ModifiedDate = DateTime.Now;
                
                await repository.UpdateAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await unitOfWork.CommitAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            throw new NotFoundException(nameof(Car), request.Id);
        }
    }
}