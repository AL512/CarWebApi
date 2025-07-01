namespace CarWebApi.Models.Cars;

public class CarLookupWithPowerDto : CarLookupDto
{
    public int Power { get; set; }
    
    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Car, CarLookupWithPowerDto>()
            .IncludeBase<Car, CarLookupDto>()
            .ForMember(carDto => carDto.Power, opt => opt.MapFrom(car => car.Pow));
    }
}