using CarWebApi.Mappings;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Элемент списка автомобиля
    /// </summary>
    public class CarLookupDto : IMapWith<Car>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// обозначение автомобиля
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarLookupDto>()
                .ForMember(carDto => carDto.Id, opt => opt.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Designation, opt => opt.MapFrom(car => $"{car.Brand.Name} {car.Name}"));
        }
    }
}
