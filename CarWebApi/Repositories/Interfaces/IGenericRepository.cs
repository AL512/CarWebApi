namespace CarWebApi.Repositories.Interfaces
{
    /// <summary>
    /// Общей интерфейс для репозиториев
    /// </summary>
    /// <typeparam name="T">Тип репозитория</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Получить по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Запрашиваемый объект</returns>
        Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить все объекты заданного типа
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция объектов</returns>
        Task<IQueryable<T>> GetAll(CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить все объекты заданного типа
        /// </summary>
        /// <returns>Коллекция объектов</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="entity">Добовляемый объект</param>
        void Add(T entity);
        /// <summary>
        /// Кобавить коллекцию объектов
        /// </summary>
        /// <param name="entities">Коллекция объектов</param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="entity">Удаляемый объект</param>
        void Remove(T entity);
        /// <summary>
        /// Удалить коллекцию объектов
        /// </summary>
        /// <param name="entities">Удаляемая коллекция объектов</param>
        void RemoveRange(IEnumerable<T> entities);
       
    }
}
