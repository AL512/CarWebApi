using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.Mappings;
using System.ComponentModel.DataAnnotations;

namespace CarWebApi.Models.Cars
{
    /// <summary>
    /// Модель данных автомобиля от клиента
    /// </summary>
    public class CreateCarDto : IMapWith<CreateCarCommand>
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// ИД марки автомобиля
        /// </summary>
        [Required]
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
        /// Маппинг модели дфнных автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCarDto, CreateCarCommand>()
                .ForMember(carCommand => carCommand.Name, opt => opt.MapFrom(carDto => carDto.Name))
                .ForMember(carCommand => carCommand.Brand.Id, opt => opt.MapFrom(carDto => carDto.BrandId))
                .ForMember(carCommand => carCommand.Pow, opt => opt.MapFrom(carDto => carDto.Pow))
                .ForMember(carCommand => carCommand.Long, opt => opt.MapFrom(carDto => carDto.Long))
                .ForMember(carCommand => carCommand.Price, opt => opt.MapFrom(carDto => carDto.Price));
        }
    }
}
