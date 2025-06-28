using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands;

    /// <summary>
    /// Обработчик команды удаления марки автомобиля
    /// </summary>
    public class DeleteBrandCommandHandler(IUnitOfWorkCarApi unitOfWork) : IRequestHandler<DeleteBrandCommand>
    {
        public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Brand>();
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
                throw new NotFoundException(nameof(Brand), request.Id);
            }
        }
    }
