using FluentValidation;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Валидация данных запроса автомобиля
    /// </summary>
    public class GetCarListQueryValidator : AbstractValidator<GetCarListQuery>
    {
        /// <summary>
        /// Валидация данных запроса автомобиля
        /// </summary>
        public GetCarListQueryValidator()
        {
            
        }
    }
}
