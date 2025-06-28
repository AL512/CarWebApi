using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Обработчик команды удаления автомобиля
    /// </summary>
    public class DeleteCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCarCommand>
    {
       
        public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Car>();
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity != null)
            {
                try
                {
                    await repository.DeleteAsync(entity, cancellationToken);
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
                await unitOfWork.RollbackAsync(cancellationToken);
                throw new NotFoundException(nameof(Car), request.Id);
            }
        }
    }
}
