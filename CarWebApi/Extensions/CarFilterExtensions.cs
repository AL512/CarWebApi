using CarWebApi.PageParams;

namespace CarWebApi.Extensions;

public static class CarFilterExtensions
{
    public static CarFilterParams WithPrice(
        this CarFilterParams spec,
        decimal? minPrice,
        decimal? maxPrice)
    {
        return spec.WithCustomFilter(car => 
            (!minPrice.HasValue || car.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || car.Price <= maxPrice.Value));
    }

}