namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Команда удаления автомобиля
    /// </summary>
    public class DeleteCarCommand : IRequest
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
    }
}
