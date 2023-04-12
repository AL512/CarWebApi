namespace CarWebApi.Models.Base
{
    /// <summary>
    /// Основные элементы для всех сущностей
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Основной идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = null;
        /// <summary>
        /// Версия сущности
        /// </summary>
        public byte[] Version { get; set; } = new byte[0];
    }
}
