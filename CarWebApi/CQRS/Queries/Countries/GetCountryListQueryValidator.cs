using CarWebApi.Queries.Countries;
using FluentValidation;

namespace CarWebApi.CQRS.Queries.Countries
{
    /// <summary>
    /// Валидация данных запроса списка стран производителей
    /// </summary>
    public class GetCountryListQueryValidator : AbstractValidator<GetCountryListQuery>
    {
        /// <summary>
        /// Валидация данных запроса списка стрнан производителей
        /// </summary>
        public GetCountryListQueryValidator()
        {
            
        }
    }
}
