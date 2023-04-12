using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.Mappings;
using CarWebApi.Models.Base;

namespace CarWebApi.Models.Countries
{
    /// <summary>
    /// Модель обновления страны производителя от клиента
    /// </summary>
    public class UpdateCountryDto : BaseEntity, IMapWith<UpdateCountryCommand>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Маппинг модели страны производителя на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCountryDto, UpdateCountryCommand>()
                .ForMember(countryCommand => countryCommand.Id, opt => opt.MapFrom(countryDto => countryDto.Id))
                .ForMember(countryCommand => countryCommand.Name, opt => opt.MapFrom(countryDto => countryDto.Name));
        }
    }
}
