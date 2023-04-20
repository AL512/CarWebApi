using CarWebApi.Database;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Repositories
{
    /// <summary>
    /// Репозиторий задаваемого типа
    /// </summary>
    /// <typeparam name="T">Тип репозитория</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CarApiDbContext _context;
        /// <summary>
        /// Репозиторий задаваемого типа
        /// </summary>
        /// <param name="context">Контекст репозитория</param>
        public GenericRepository(CarApiDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public async Task<IQueryable<T>> GetAll(CancellationToken cancellationToken)
        {
            return _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public async Task<T> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> ToIQueryable(IEnumerable<T> values)
        {
            return values.AsQueryable();
        }
    }
}
