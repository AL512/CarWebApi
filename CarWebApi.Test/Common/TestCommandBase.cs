using CarWebApi.Database;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.Test.Common
{
    /// <summary>
    /// Создание контекста БД для тестирования команд
    /// </summary>
    public abstract class TestCommandBase : IDisposable
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Создаёт контекст БД для тестирования команд
        /// </summary>
        public TestCommandBase()
        {
            UnitOfWork = СarApiContextFactory.Create();
        }
        /// <summary>
        /// Удаляем контекст БД для тестирования команд
        /// </summary>
        public void Dispose()
        {
            СarApiContextFactory.Destroy(UnitOfWork);
        }
    }
}
