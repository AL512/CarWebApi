using CarWebApi.Models.Cars;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Запрос списка автомобиля
    /// </summary>
    public class GetCarListQuery : IRequest<CarList>
    {
        //Дополнительная логика. Фильтрация по ролям и пр.
    }
}
