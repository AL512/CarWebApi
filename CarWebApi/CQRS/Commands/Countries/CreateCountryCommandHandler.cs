using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Countries;

/// <summary>
/// Обработчик команды добавления страны производителя
/// </summary>
public class CreateCountryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCountryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCountryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Country
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreationDate = DateTime.Now,
            ModifiedDate = null,
        };

        var repository = unitOfWork.GetRepository<Country>();
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