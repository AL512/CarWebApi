using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands;

/// <summary>
/// Обработчик команды изменения марки авто
/// </summary>
public class UpdateBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBrandCommand>
{
    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Brand>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity != null)
        {
            try
            {
                entity.Name = request.Name;
                entity.CountryId = request.CountryId;
                entity.Country = request.Country;
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
            throw new NotFoundException(nameof(Brand), request.Id);
        }
    }
}