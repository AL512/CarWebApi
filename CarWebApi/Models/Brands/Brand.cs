using CarWebApi.Models.Base;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Марки автомобилей
    /// </summary>
    public class Brand : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Страна производитель
        /// </summary>
        public virtual Country Country { get; set; }
        /// <summary>
        /// ИД страна производителя
        /// </summary>
        public virtual Guid? CountryId { get; set; }
        /// <summary>
        /// Список автомобилей этой марки
        /// </summary>
        public virtual ICollection<Car> Cars { get; set; }
    }
}
