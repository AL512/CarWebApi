namespace CarWebApi.Exceptions
{
    /// <summary>
    /// Ошибка нахождения сущности в БД
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Ошибка нахождения сущности в БД
        /// </summary>
        /// <param name="name">Название сущности</param>
        /// <param name="key">Параметр сущности</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found.") { }
    }
}
