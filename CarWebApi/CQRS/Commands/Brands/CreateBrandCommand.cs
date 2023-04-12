using CarWebApi.Models.Countries;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Команда добавления марки авто
    /// </summary>
    public class CreateBrandCommand : IRequest<Guid>
    {
        /// <summary>
        /// Название марки авто
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; }

    }
}
