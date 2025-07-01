using CarWebApi.Models.Cars;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Запрос списка автомобиля
    /// </summary>
    public class GetCarListQuery : IRequest<List<CarLookupDto>>
    {
        //Дополнительная логика. Фильтрация по ролям и пр.
    }
}
