using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Команда обновления автомобиля
    /// </summary>
    public class UpdateCarCommand : IRequest
    {
        /// <summary>
        /// ИД 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название авто
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД марки авто
        /// </summary>
        public Brand Brand { get; set; }
        /// <summary>
        /// Мощность двигателя
        /// </summary>
        public int Pow { get; set; }
        /// <summary>
        /// Длина кузова авто
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// Цена авто
        /// </summary>
        public decimal Price { get; set; }
    }
}
