using CarWebApi.Mappings;
using CarWebApi.Models.Base;
using CarWebApi.Models.Countries;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Для отображения марки автомобиля
    /// </summary>
    public class BrandDetails : BaseEntity, IMapWith<Brand>
    {
        /// <summary>
        /// Основной идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid CountryId { get; set; }
        
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Brand, BrandDetails>()
                .ForMember(brandDetails => brandDetails.Id, opt => opt.MapFrom(brand => brand.Id))
                .ForMember(brandDetails => brandDetails.Name, opt => opt.MapFrom(brand => brand.Name))
                .ForMember(brandDetails => brandDetails.CountryId, opt => opt.MapFrom(brand => brand.Country.Id))
                .ForMember(brandDetails => brandDetails.CreationDate, opt => opt.MapFrom(brand => brand.CreationDate))
                .ForMember(brandDetails => brandDetails.ModifiedDate, opt => opt.MapFrom(brand => brand.ModifiedDate));
        }
    }
}
