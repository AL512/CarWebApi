﻿using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Mappings;
using CarWebApi.Models.Base;
using CarWebApi.Models.Countries;

namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Модель обновления марки автомобиля от клиента
    /// </summary>
    public class UpdateBrandDto : IMapWith<UpdateBrandCommand>
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
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; }
        /// <summary>
        /// Маппинг модели марки автомобиля на команду создания
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBrandDto, UpdateBrandCommand>()
                .ForMember(brandCommand => brandCommand.Id, opt => opt.MapFrom(brandDto => brandDto.Id))
                .ForMember(brandCommand => brandCommand.Name, opt => opt.MapFrom(brandDto => brandDto.Name))
                .ForMember(brandCommand => brandCommand.Country, opt => opt.MapFrom(brandDto => brandDto.Country));
        }
    }
}
