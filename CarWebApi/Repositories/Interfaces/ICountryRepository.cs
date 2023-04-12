using CarWebApi.Models.Countries;

namespace CarWebApi.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория стран производителей
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Дополнительная логика, связанная с этим типом репозитория.
    }
}
