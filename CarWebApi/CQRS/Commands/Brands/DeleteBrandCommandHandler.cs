using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Обработчик команды удаления марки автомобиля
    /// </summary>
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Обработчик команды удаления марки автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;
        /// <summary>
        /// Логика обработки команды DeleteCarCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
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
                    UnitOfWork.Brands.Remove(entity);
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
