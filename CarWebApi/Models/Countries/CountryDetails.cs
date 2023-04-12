using CarWebApi.Mappings;
using CarWebApi.Models.Base;

namespace CarWebApi.Models.Countries
{
    /// <summary>
    /// Отображение детальной информации о стране производителе
    /// </summary>
    public class CountryDetails : BaseEntity, IMapWith<Country>
    {

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDetails>()
                .ForMember(countryDetails => countryDetails.Id, opt => opt.MapFrom(country => country.Id))
                .ForMember(countryDetails => countryDetails.Name, opt => opt.MapFrom(country => country.Name))
                .ForMember(countryDetails => countryDetails.CreationDate, opt => opt.MapFrom(country => country.CreationDate))
                .ForMember(countryDetails => countryDetails.ModifiedDate, opt => opt.MapFrom(country => country.ModifiedDate));
        }
    }
}
