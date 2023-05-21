using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Mappings;
using CarWebApi.Models.Countries;
using System.ComponentModel.DataAnnotations;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Модель данных марки автомобиля от клиента
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
        /// </summary>и
        public Guid CountryId { get; set; }
        /// <summary>
        /// Маппинг модели марки автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBrandDto, CreateBrandCommand>()
                .ForMember(brandCommand => brandCommand.Name, opt => opt.MapFrom(brandDto => brandDto.Name))
                .ForMember(brandCommand => brandCommand.CountryId, opt => opt.MapFrom(brandDto => brandDto.CountryId));
        }
    }
}
