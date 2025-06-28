using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands
{
    public class CreateBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand entity = new Brand
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CountryId = request.CountryId,
                Country = request.Country,
                CreationDate = DateTime.Now,
                ModifiedDate = null,
            };
            
            var repository = unitOfWork.GetRepository<Brand>();
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
}
