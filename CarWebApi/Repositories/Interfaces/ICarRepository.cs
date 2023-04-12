using CarWebApi.Models.Cars;

namespace CarWebApi.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория автомобилей
    /// </summary>
    public interface ICarRepository : IGenericRepository<Car>
    {
        //Дополнительная логика, связанная с этим типом репозитория.
    }
}
