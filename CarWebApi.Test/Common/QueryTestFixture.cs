using CarWebApi.Database;
using CarWebApi.Mappings;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Test.Common
{
    /// <summary>
    /// Вспомогательный класс проверки запросов
    /// </summary>
    public class QueryTestFixture : IDisposable
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        public IUnitOfWork UnitOfWork;
        /// <summary>
        /// Маппер
        /// </summary>
        public IMapper Mapper;

        /// <summary>
        /// Вспомогательный класс проверки запросов
        /// </summary>
        public QueryTestFixture()
        {
            UnitOfWork = СarApiContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(CarApiDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }
        /// <summary>
        /// Освобождает ресурс
        /// </summary>
        public void Dispose()
        {
            СarApiContextFactory.Destroy(UnitOfWork);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}
