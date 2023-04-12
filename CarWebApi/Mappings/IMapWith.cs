using AutoMapper;

namespace CarWebApi.Mappings
{
    /// <summary>
    /// Интерфейс для маппинга типов
    /// </summary>
    /// <typeparam name="T">дженерик</typeparam>
    public interface IMapWith<T>
    {
        /// <summary>
        /// Создает конфигурацию из типа Т тип назначения
        /// </summary>
        /// <param name="profile">Предоставляет именованную конфигурацию для маппов</param>
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
