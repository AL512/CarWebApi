using CarWebApi.Database;
using CarWebApi.Database;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWebApi.Test.Common
{
    /// <summary>
    /// Фабрика контекста БД для тестирования
    /// </summary>
    public static class СarApiContextFactory
    {
        /// <summary>
        /// ИД пользователя А
        /// </summary>
        public static Guid UserAId = Guid.NewGuid();
        /// <summary>
        /// ИД пользователя В
        /// </summary>
        public static Guid UserBId = Guid.NewGuid();

        /// <summary>
        /// ИД страны производителя для удаления
        /// </summary>
        public static Guid CountryIdForDelete = Guid.NewGuid();
        /// <summary>
        /// ИД страны производителя для обновления
        /// </summary>
        public static Guid CountryIdForUpdate = Guid.NewGuid();

        /// <summary>
        /// ИД марки автомобиля для удаления
        /// </summary>
        public static Guid BrandIdForDelete = Guid.NewGuid();
        /// <summary>
        /// ИД марки автомобиля для обновления
        /// </summary>
        public static Guid BrandIdForUpdate = Guid.NewGuid();

        /// <summary>
        /// ИД автомобиля для удаления
        /// </summary>
        public static Guid CarIdForDelete = Guid.NewGuid();
        /// <summary>
        /// ИД автомобиля для обновления
        /// </summary>
        public static Guid CarIdForUpdate = Guid.NewGuid();

        /// <summary>
        /// Создаем контекст ДБ для тестирования
        /// </summary>
        /// <returns>Контекст ДБ для тестирования</returns>
        public static UnitOfWork Create()
        {
            var options = new DbContextOptionsBuilder<CarApiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;
            var context = new CarApiDbContext(options);
            context.Database.EnsureCreated();

            UnitOfWork unitOfWork = new UnitOfWork(context);

            AddEntities(unitOfWork);
            context.SaveChanges();
            return unitOfWork;
        }
        /// <summary>
        /// Добавляет сущности для тестирования работы с БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        private static void AddEntities(IUnitOfWork unitOfWork)
        {
            var bd = new DataBuilder();
            
            var Russia = new Country
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("C0D63113-83D5-4443-A303-07250D2E0F75"),
                Name = "Russia" 
            };
            var Japan = new Country
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("44AB00A9-EEF2-4887-AA5F-8C55FB7DA3F9"),
                Name = "Japan"
            };
            var USA = new Country
            {
                CreationDate = DateTime.Today,
                Id = CountryIdForDelete,
                Name = "USA"
            };
            var Korea = new Country
            {
                CreationDate = DateTime.Today,
                Id = CountryIdForUpdate,
                Name = "Korea"
            };

            unitOfWork?.Countries?.AddRange(
                new List<Country> {
                    Russia,
                    Japan,
                    USA,
                    Korea});

            var LADA = new Brand
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("57E34833-8697-4534-A2B7-C2CD49259F91"),
                Name = "LADA",
                Country = Russia
            };
            var Toyota = new Brand
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("339CBD77-CDD6-44A2-9FE9-85C9CD82D9CA"),
                Name = "Toyota",
                Country = Japan
            };
            var Nissan = new Brand
            {
                CreationDate = DateTime.Today,
                Id = BrandIdForDelete,
                Name = "Nissan",
                Country = Japan
            };
            var Mitsubishi = new Brand
            {
                CreationDate = DateTime.Today,
                Id = BrandIdForUpdate,
                Name = "Mitsubishi",
                Country = Japan
            };

            unitOfWork?.Brands?.AddRange(
                new List<Brand> {
                    LADA,
                    Toyota,
                    Nissan,
                    Mitsubishi });

            var Largus = new Car
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("12B08667-2E05-4DE7-8916-BCC3E46A9959"),
                Name = "Largus",
                Brand = LADA,
                Pow = 87,
                Long = 4470,
                Price = 558,
            };

            var Corolla = new Car
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("3DF663A4-1DE6-4A83-BB1F-C75FB32D74A5"),
                Name = "Corolla",
                Brand = Toyota,
                Pow = 122,
                Long = 4300,
                Price = 1173,
            };
            var Camry = new Car
            {
                CreationDate = DateTime.Today,
                Id = Guid.Parse("00385A0C-012D-4777-96E8-0D239937F087"),
                Name = "Camry",
                Brand = Toyota,
                Pow = 249,
                Long = 4500,
                Price = 1573,
            };
            var Tuscon = new Car
            {
                CreationDate = DateTime.Today,
                Id = CarIdForDelete,
                Name = "Tuscon",
                Brand = Mitsubishi,
                Pow = 185,
                Long = 4620,
                Price = 1499,
            };
            var Granta = new Car
            {
                CreationDate = DateTime.Today,
                Id = CarIdForUpdate,
                Name = "Granta",
                Brand = LADA,
                Pow = 87,
                Long = 4260,
                Price = 404,
            };

            unitOfWork?.Cars?.AddRange(
                new List<Car> {
                    Largus,
                    Corolla,
                    Camry,
                    Tuscon,
                    Granta });
        }

        /// <summary>
        /// Удаляем контекст ДБ для тестирования
        /// </summary>
        /// <param name="UnitOfWork">Менеджер репозиториев для тестирования</param>
        public static void Destroy(IUnitOfWork UnitOfWork)
        {
            UnitOfWork.Dispose();
        }
    }
}
