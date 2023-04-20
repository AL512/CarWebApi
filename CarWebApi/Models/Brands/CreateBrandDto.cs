using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Mappings;
using CarWebApi.Models.Countries;
using System.ComponentModel.DataAnnotations;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Модель данных модели автомобиля от клиента
    /// </summary>
    public class CreateBrandDto : IMapWith<CreateBrandCommand>
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid CountryId { get; set; }
        /// <summary>
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; }
        /// <summary>
        /// Маппинг модели марки автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBrandDto, CreateBrandCommand>()
                .ForMember(brandCommand => brandCommand.Name, opt => opt.MapFrom(brandDto => brandDto.Name))
                .ForMember(brandCommand => brandCommand.Country, opt => opt.MapFrom(brandDto => brandDto.Country)); ;
        }
    }
}
