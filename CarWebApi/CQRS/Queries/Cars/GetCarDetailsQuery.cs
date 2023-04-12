using CarWebApi.Models.Cars;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Получение автомобиля
    /// </summary>
    public class GetCarDetailsQuery : IRequest<CarDetails>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
    }
}
