using AutoMapper;
using CarWebApi.Mappings;
using CarWebApi.Models.Cars;

namespace CarWebApi.Models.Countries
{
    /// <summary>
    /// Элемент списка стран производителей (DTO)
    /// </summary>
    public class CountryLookupDto : IMapWith<Country>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryLookupDto>()
                .ForMember(countryDto => countryDto.Id, opt => opt.MapFrom(country => country.Id))
                .ForMember(countryDto => countryDto.Name, opt => opt.MapFrom(country => country.Name));
        }
    }
}
