using Microsoft.EntityFrameworkCore.Storage;

namespace CarWebApi.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс менеджера репозиториев
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий стран производителей
        /// </summary>
        ICountryRepository Countries { get; }
        /// <summary>
        /// Репозиторий марок автомобилей
        /// </summary>
        IBrandRepository Brands { get; }
        /// <summary>
        /// Репозиторий автомобилей
        /// </summary>
        ICarRepository Cars { get; }
        /// <summary>
        /// Начало транзакции
        /// </summary>
        /// <returns>Транзакция</returns>
        public IDbContextTransaction BeginTransaction();
        /// <summary>
        /// Фиксирует все изменения, внесенные в базу данных в текущей транзакции.
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        public void CommitTransaction(IDbContextTransaction transaction);
        /// <summary>
        /// Отменяет все изменения, внесенные в базу данных в текущей транзакции.
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        public void RollbackTransaction(IDbContextTransaction transaction);
        /// <summary>
        /// Сохранение данных
        /// </summary>
        public int Save();
    }
}
