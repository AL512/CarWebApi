using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Обработчик команды добавления марки автотомобиля
    /// </summary>
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Обработчик команды добавления марки автотомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public CreateBrandCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды CreateCarCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>ИД марки автотомобиля</returns>
        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Country = request.Country,
                CreationDate = DateTime.Now,
                ModifiedDate = null,
            };

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    UnitOfWork.Brands.Add(brand);
                    UnitOfWork.Save();
                    UnitOfWork.CommitTransaction(transaction);
                }catch(Exception ex)
                {
                    UnitOfWork.RollbackTransaction(transaction);
                    throw ex;
                }
            }

            return brand.Id;
        }
    }
}
