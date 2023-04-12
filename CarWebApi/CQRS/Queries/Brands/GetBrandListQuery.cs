using CarWebApi.Models.Brands;

namespace CarWebApi.CQRS.Queries.Brands
{
    /// <summary>
    /// Запрос списка марок автомобиля
    /// </summary>
    public class GetBrandListQuery : IRequest<BrandList>
    {
        //Дополнительная логика. Фильтрация по ролям и пр.
    }
}
