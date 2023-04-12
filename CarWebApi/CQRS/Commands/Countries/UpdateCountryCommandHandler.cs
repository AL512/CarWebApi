using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Обработчик команды изменения страны производителя
    /// </summary>
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Конструктор обработчика команды изменения страны производителя
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public UpdateCountryCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды UpdateCountryCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены задачи</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Countries.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    entity.Name = request.Name;
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
