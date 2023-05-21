using CarWebApi.Models.Brands;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Команда добавления модели автомобиля
    /// </summary>
    ///<remarks>
    /// Марка автомобиля задается свойством 'Brand'
    /// Если 'Brand' не задан, то привязка происходит по 'BrandId'
    /// </remarks>
    public class CreateCarCommand : IRequest<Guid>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Мощность двигателя
        /// </summary>
        public int Pow { get; set; }
        /// <summary>
        /// Длина кузова
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ИД марки
        /// </summary>
        public Guid? BrandId { get; set; } = null;
        /// <summary>
        /// Марка
        /// </summary>
        public Brand Brand { get; set; } = null;
    }
}
