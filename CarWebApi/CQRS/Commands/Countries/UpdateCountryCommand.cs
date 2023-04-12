namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Команда обновления страны производителя
    /// </summary>
    public class UpdateCountryCommand : IRequest
    {
        /// <summary>
        /// ИД 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название страны
        /// </summary>
        public string Name { get; set; }
    }
}
