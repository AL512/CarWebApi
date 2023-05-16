using CarWebApi.Models.Countries;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Команда добавления марки автомобиля
    /// </summary>
    /// <remarks>
    /// Страна производитель задается свойством 'Country'
    /// Если 'Country' не задан, то привязка происходит по 'CountryId'
    /// </remarks>
    public class CreateBrandCommand : IRequest<Guid>
    {
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
        public Country Country { get; set; }
    }
}
