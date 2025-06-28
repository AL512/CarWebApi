using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Countries;

/// <summary>
/// Обработчик команды изменения страны производителя
/// </summary>
public class UpdateCountryCommandHandler(IUnitOfWorkCarApi unitOfWork) : IRequestHandler<UpdateCountryCommand>
{
    public async Task Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Country>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity != null)
        {
            try
            {
                entity.Name = request.Name;
                entity.ModifiedDate = DateTime.Now;

                await repository.UpdateAsync(entity, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}