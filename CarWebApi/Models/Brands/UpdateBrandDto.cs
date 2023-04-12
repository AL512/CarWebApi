using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Mappings;
using CarWebApi.Models.Base;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Модель обновления марки автомобиля от клиента
    /// </summary>
    public class UpdateBrandDto : BaseEntity, IMapWith<UpdateBrandCommand>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Маппинг модели марки автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBrandDto, UpdateBrandCommand>()
                .ForMember(brandCommand => brandCommand.Id, opt => opt.MapFrom(brandDto => brandDto.Id))
                .ForMember(brandCommand => brandCommand.Name, opt => opt.MapFrom(brandDto => brandDto.Name))
                .ForMember(brandCommand => brandCommand.Country.Id, opt => opt.MapFrom(brandDto => brandDto.CountryId));
        }
    }
}
