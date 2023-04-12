using FluentValidation;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Валидация данных запроса автомобиля
    /// </summary>
    public class GetCarDetailsQueryValidator : AbstractValidator<GetCarDetailsQuery>
    {
        /// <summary>
        /// Валидация данных запроса автомобиля
        /// </summary>
        public GetCarDetailsQueryValidator()
        {
            RuleFor(car => car.Id).NotEqual(Guid.Empty);
        }
    }
}
