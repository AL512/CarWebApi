using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.Mappings;
using CarWebApi.Models.Base;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Модель обновления автомобиля от клиента
    /// </summary>
    public class UpdateCarDto : BaseEntity, IMapWith<UpdateCarCommand> 
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД марки автомобиля
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
        /// Маппинг модели автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCarDto, UpdateCarCommand>()
                .ForMember(carCommand => carCommand.Id, opt => opt.MapFrom(carDto => carDto.Id))
                .ForMember(carCommand => carCommand.Name, opt => opt.MapFrom(carDto => carDto.Name))
                .ForMember(carCommand => carCommand.Brand.Id, opt => opt.MapFrom(carDto => carDto.BrandId))
                .ForMember(carCommand => carCommand.Pow, opt => opt.MapFrom(carDto => carDto.Pow))
                .ForMember(carCommand => carCommand.Long, opt => opt.MapFrom(carDto => carDto.Long))
                .ForMember(carCommand => carCommand.Price, opt => opt.MapFrom(carDto => carDto.Price));
        }
    }
}
