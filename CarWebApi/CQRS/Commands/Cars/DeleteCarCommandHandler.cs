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
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Конструктор обработчика команды удаления автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public DeleteCarCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;
        /// <summary>
        /// Логика обработки команды DeleteCarCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Cars.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    UnitOfWork.Cars.Remove(entity);
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
