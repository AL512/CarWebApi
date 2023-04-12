﻿using CarWebApi.Models.Countries;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Команда обновления марки авто
    /// </summary>
    public class UpdateBrandCommand : IRequest
    {
        /// <summary>
        /// ИД 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название марки авто
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Страна производитель
        /// </summary>
        public Country Country { get; set; }
    }
}
