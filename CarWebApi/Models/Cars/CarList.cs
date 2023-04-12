using СarDealership.Application.Cars.Queries.GetBrandList;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Список автомобиля
    /// </summary>
    public class CarList
    {
        /// <summary>
        /// Список автомобиля
        /// </summary>
        public ICollection<CarLookupDto> Cars { get; set; }
    }
}
