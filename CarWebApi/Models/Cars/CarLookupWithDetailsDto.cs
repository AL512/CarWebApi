namespace CarWebApi.Models.Cars;

public class CarLookupWithDetailsDto : CarLookupDto
{
    public int Power { get; set; }
    public decimal Price { get; set; }
    
    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Car, CarLookupWithDetailsDto>()
            .IncludeBase<Car, CarLookupDto>()
            .ForMember(carDto => carDto.Power, opt => opt.MapFrom(car => car.Pow))
            .ForMember(carDto => carDto.Price, opt => opt.MapFrom(car => car.Price));
    }
}