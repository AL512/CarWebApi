using CarWebApi.Models.Countries;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Команда обновления марки автомобиля
    /// </summary>
    public class UpdateBrandCommand : IRequest
    {
        /// <summary>
        /// ИД 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название марки авто
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid CountryId { get; set; }
        /// <summary>
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; }
    }
}
