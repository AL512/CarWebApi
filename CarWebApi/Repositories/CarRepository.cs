using CarWebApi.Database;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Repositories
{
    /// <summary>
    /// Репозиторий автомобилей
    /// </summary>
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        /// <summary>
        /// Репозиторий автомобилей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public CarRepository(CarApiDbContext context) : base(context)
        {
        }
    }
}
