using CarWebApi.Database;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarWebApi.Repositories
{
    /// <summary>
    /// Менеджер репозиториев
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarApiDbContext _context;
        public ICountryRepository Countries { get; private set; }
        public IBrandRepository Brands { get; private set; }
        public ICarRepository Cars { get; private set; }

        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public UnitOfWork(CarApiDbContext context)
        {
            _context = context;
            Countries = new CountryRepository(_context);
            Brands = new BrandRepository(_context);
            Cars = new CarRepository(_context);
        }

        public IDbContextTransaction BeginTransaction()
        {
           return _context.Database.BeginTransaction();
        }

        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.CommitAsync();
        }
        public int Save()
        {          
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void RollbackTransaction(IDbContextTransaction transaction)
        {
            transaction?.RollbackAsync();
        }
    }
}
