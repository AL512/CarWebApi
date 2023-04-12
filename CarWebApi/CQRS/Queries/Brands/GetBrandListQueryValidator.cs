using CarWebApi.CQRS.Queries.Brands;
using FluentValidation;

namespace СarDealership.Application.Cars.Queries.GetBrandList
{
    /// <summary>
    /// Валидация данных запроса списка марок автомобиля
    /// </summary>
    public class GetBrandListQueryValidator : AbstractValidator<GetBrandListQuery>
    {
        /// <summary>
        /// Валидация данных запроса списка марок автомобиля
        /// </summary>
        public GetBrandListQueryValidator()
        {
            
        }
    }
}
