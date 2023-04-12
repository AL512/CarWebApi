using FluentValidation;

namespace CarWebApi.CQRS.Queries.Countries
{
    /// <summary>
    /// Валидация данных запроса страны производителя
    /// </summary>
    public class GetCountryQueryValidator : AbstractValidator<GetCountryDetailsQuery>
    {
        /// <summary>
        /// Валидация данных запроса страны производителя
        /// </summary>
        public GetCountryQueryValidator()
        {
            RuleFor(country => country.Id).NotEqual(Guid.Empty);
        }
    }
}
