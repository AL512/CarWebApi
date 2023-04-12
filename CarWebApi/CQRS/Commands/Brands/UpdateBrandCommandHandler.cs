using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Обработчик команды изменения марки авто
    /// </summary>
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Обработчик команды изменения марки авто
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды UpdateBrandCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены задачи</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Brands.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {

                    entity.Name = request.Name;
                    entity.Country = request.Country;

                    entity.ModifiedDate = DateTime.Now;
            
                    UnitOfWork.Save();
                    UnitOfWork.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    UnitOfWork.RollbackTransaction(transaction);
                    throw ex;
                }
            }
        }

    }
}
