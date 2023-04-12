using CarWebApi.Models;
using CarWebApi.Models.Countries;

namespace CarWebApi.CQRS.Queries.Countries
{
    /// <summary>
    /// Получение данных о стране производителе
    /// </summary>
    public class GetCountryDetailsQuery : IRequest<CountryDetails>
    {
        /// <summary>
        /// ИД страны производителя
        /// </summary>
        public Guid Id { get; set; }
    }
}
