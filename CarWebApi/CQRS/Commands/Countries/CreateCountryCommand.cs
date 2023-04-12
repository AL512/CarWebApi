namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Команда добавления страны производителя
    /// </summary>
    public class CreateCountryCommand : IRequest<Guid>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
