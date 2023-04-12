using CarWebApi.Database;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Repositories
{
    /// <summary>
    /// Репозиторий стран производителей
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        /// <summary>
        /// Репозиторий стран производителей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public CountryRepository(CarApiDbContext context) : base(context)
        {
        }
    }
}
