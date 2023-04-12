using CarWebApi.Models.Base;
using CarWebApi.Models.Brands;

namespace CarWebApi.Models.Countries
{
    /// <summary>
    /// Страна производитель автомобилей
    /// </summary>
    public class Country : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список марок авто этой страны
        /// </summary>
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
