using CarWebApi.Mappings;
using CarWebApi.Models.Base;
using CarWebApi.Models.Brands;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Для отображения автомобиля
    /// </summary>
    public class CarDetails : IMapWith<Car>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД марки
        /// </summary>
        public Guid BrandId { get; set; }
        /// <summary>
        /// Мощность двигателя
        /// </summary>
        public int Pow { get; set; }
        /// <summary>
        /// Длина кузова
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarDetails>()
                .ForMember(carDetails => carDetails.Id, opt => opt.MapFrom(car => car.Id))
                .ForMember(carDetails => carDetails.Name, opt => opt.MapFrom(car => car.Name))
                .ForMember(carDetails => carDetails.BrandId, opt => opt.MapFrom(car => car.Brand.Id))
                .ForMember(carDetails => carDetails.Pow, opt => opt.MapFrom(car => car.Pow))
                .ForMember(carDetails => carDetails.Long, opt => opt.MapFrom(car => car.Long))
                .ForMember(carDetails => carDetails.Price, opt => opt.MapFrom(car => car.Price))
                .ForMember(carDetails => carDetails.CreationDate, opt => opt.MapFrom(car => car.CreationDate))
                .ForMember(carDetails => carDetails.ModifiedDate, opt => opt.MapFrom(car => car.ModifiedDate));
        }
    }
}
