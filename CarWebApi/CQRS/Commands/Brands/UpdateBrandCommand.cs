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
        public string Name { get; set; } = null!;
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid? CountryId { get; set; } = null;
        /// <summary>
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; } = null;
    }
}
