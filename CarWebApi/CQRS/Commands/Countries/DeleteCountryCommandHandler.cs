using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Countries;

/// <summary>
/// Обработчик команды удаления страны производителя
/// </summary>
public class DeleteCountryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCountryCommand>
{
    public async Task Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Country>();
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
            throw new NotFoundException(nameof(Country), request.Id);
        }
    }
}