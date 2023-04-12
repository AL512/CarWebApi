using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.Mappings;
using System.ComponentModel.DataAnnotations;

namespace CarWebApi.Models.Countries
{
    public class CreateCountryDto : IMapWith<CreateCountryCommand>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Маппинг модели дфнных автомобиля на команду создания
        /// </summary>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCountryDto, CreateCountryCommand>()
                .ForMember(countryCommand => countryCommand.Name, opt => opt.MapFrom(countryDto => countryDto.Name));
        }
    }
}
