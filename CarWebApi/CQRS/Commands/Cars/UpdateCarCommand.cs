using CarWebApi.Models.Brands;

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
        public string Name { get; set; } = null!;
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
        /// <summary>
        /// ИД марки автомобиля
        /// </summary>
        public Guid? BrandId { get; set; } = null;
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public Brand Brand { get; set; } = null;
    }
}
