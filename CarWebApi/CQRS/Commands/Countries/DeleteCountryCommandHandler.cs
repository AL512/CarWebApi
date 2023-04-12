using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Обработчик команды удаления страны производителя
    /// </summary>
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Конструктор обработчика команды удаления страны производителя
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды DeleteCountryCommandHandler
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пустой ответ</returns>
        public async Task Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
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
                    UnitOfWork.Countries.Remove(entity);
                    UnitOfWork.Save();
                    UnitOfWork.CommitTransaction(transaction);
                }
                catch(Exception ex)
                {
                    UnitOfWork.RollbackTransaction(transaction);
                    throw ex;
                }
            }
        }
    }
}
