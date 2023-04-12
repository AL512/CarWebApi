using CarWebApi.Database;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Repositories
{
    /// <summary>
    /// Репозиторий марок автомобилей
    /// </summary>
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        /// <summary>
        /// Репозиторий марок автомобилей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public BrandRepository(CarApiDbContext context) : base(context)
        {
        }
    }
}
