using CarWebApi.Mappings;
using CarWebApi.Models.Base;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Элемент списка марок автомодилей
    /// </summary>
    public class BrandLookupDto :  IMapWith<Brand>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Обозначение марки автомобилей
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// Маппинг
        /// </summary>
        /// <param name="profile">Профиль маппинга</param>

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Brand, BrandLookupDto>()
                .ForMember(brandDto => brandDto.Id, opt => opt.MapFrom(brand => brand.Id))
                .ForMember(brandDto => brandDto.Designation, opt => opt.MapFrom(brand => $"{brand.Name} ({brand.Country.Name})"));
        }
    }
}
