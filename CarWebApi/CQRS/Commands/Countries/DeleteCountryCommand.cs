namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Команда удаления страны производителя
    /// </summary>
    public class DeleteCountryCommand : IRequest
    {
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid Id { get; set; }
    }
}
