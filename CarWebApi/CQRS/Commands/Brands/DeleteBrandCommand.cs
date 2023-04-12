namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Команда удаления марки авто
    /// </summary>
    public class DeleteBrandCommand : IRequest
    {
        /// <summary>
        /// ИД страны марки авто
        /// </summary>
        public Guid Id { get; set; }
    }
}
