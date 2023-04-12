using FluentValidation;

namespace CarWebApi.CQRS.Queries.Brands
{
    /// <summary>
    /// Валидация данных запроса марки автомобиля
    /// </summary>
    public class GetBrandDetailsQueryValidator : AbstractValidator<GetBrandDetailsQuery>
    {
        /// <summary>
        /// Валидация данных запроса марки автомобиля
        /// </summary>
        public GetBrandDetailsQueryValidator()
        {
            RuleFor(brand => brand.Id).NotEqual(Guid.Empty);
        }
    }
}
