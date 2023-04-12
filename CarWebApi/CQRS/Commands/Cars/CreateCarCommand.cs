using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Команда добавления модели автомобиля
    /// </summary>
    public class CreateCarCommand : IRequest<Guid>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// ИД марки
        /// </summary>
        public Brand Brand { get; set; } = null!;
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
    }
}
