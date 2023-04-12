using CarWebApi.Models.Brands;

namespace CarWebApi.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория марок автомобилей
    /// </summary>
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        //Дополнительная логика, связанная с этим типом репозитория.
    }
}
