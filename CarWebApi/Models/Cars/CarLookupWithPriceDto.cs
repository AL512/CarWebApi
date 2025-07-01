using CarWebApi.Mappings;

namespace CarWebApi.Models.Cars;

public class CarLookupWithPriceDto : CarLookupDto
{
    public decimal Price { get; set; }
    
    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Car, CarLookupWithPriceDto>()
            .IncludeBase<Car, CarLookupDto>()
            .ForMember(carDto => carDto.Price, opt => opt.MapFrom(car => car.Price));
    }
}