using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Обработчик команды изменения автомобиля
    /// </summary>
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Конструктор обработчика команды изменения автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public UpdateCarCommandHandler(IUnitOfWork unitOfWork   ) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды UpdateCarCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены задачи</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Cars.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            entity.ModifiedDate = DateTime.Now;
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    entity.Name = request.Name;
                    entity.Brand = request.Brand;
                    entity.Pow = request.Pow;
                    entity.Long = request.Long;
                    entity.Price = request.Price;
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
