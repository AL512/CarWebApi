using CarWebApi.Models.Base;
using CarWebApi.Models.Brands;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Автомобиль
    /// </summary>
    public class Car : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
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
        /// ИД марка
        /// </summary>
        public Guid? BrandId { get; set; }
        /// <summary>
        /// Марка
        /// </summary>
        public virtual Brand Brand { get; set; }
    }
}
