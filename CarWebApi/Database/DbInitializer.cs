using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;

namespace CarWebApi.Database
{
    // <summary>
    /// Инициализация БД
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// Инициализация БД
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public static void Initialize(CarApiDbContext context)
        {
            if (!context.Countries.Any())
            {
                AddData(context);
            }
        }
        /// <summary>
        /// Добавление данных в БД
        /// </summary>
        /// <param name="context">Контекст Бд</param>
        static void AddData(CarApiDbContext context)
        {
            var Russia = new Country
            {
                Name = "Россия"
            };
            var Japan = new Country
            {
                Name = "Япония"
            };
            var Korea = new Country
            {
                Name = "Корея"
            };
            context.Countries.AddRange(
                Russia,
                Japan,
                Korea
                );

            var LADA = new Brand
            {
                Name = "LADA",
                Country = Russia
            };
            var Toyota = new Brand
            {
                Name = "Toyota",
                Country = Japan
            };
            var Nissan = new Brand
            {
                Name = "Nissan",
                Country = Japan
            };
            var Mitsubishi = new Brand
            {
                Name = "Mitsubishi",
                Country = Japan
            };
            var Hyundai = new Brand
            {
                Name = "Hyundai",
                Country = Korea
            };
            var KIA = new Brand
            {
                Name = "KIA",
                Country = Korea
            };

            context.Brands.AddRange(
                LADA,
                Toyota,
                Nissan,
                Mitsubishi,
                Hyundai,
                KIA);

            Car[] cars = new Car[19]
            {
                new Car
                {
                    Name = "Rio",
                    Brand = KIA,
                    Pow = 100,
                    Long = 4400,
                    Price = 734,
                },
                new Car
                {
                    Name = "Ceed",
                    Brand = KIA,
                    Pow = 128,
                    Long = 4340,
                    Price = 1039
                },
                new Car
                {
                    Name = "Sorento",
                    Brand = KIA,
                    Pow = 175,
                    Long = 4670,
                    Price = 1544
                },
                new Car
                {
                    Name = "Solaris",
                    Brand = Hyundai,
                    Pow = 99,
                    Long = 4430,
                    Price = 746
                },
                new Car
                {
                    Name = "Elantra",
                    Brand = Hyundai,
                    Pow = 128,
                    Long = 4580,
                    Price = 1059
                },
                new Car
                {
                    Name = "Tuscon",
                    Brand = Hyundai,
                    Pow = 185,
                    Long = 4620,
                    Price = 1499
                },
                new Car
                {
                    Name = "Granta",
                    Brand = LADA,
                    Pow = 87,
                    Long = 4260,
                    Price = 404
                },
                new Car
                {
                    Name = "Vesta",
                    Brand = LADA,
                    Pow = 106,
                    Long = 4410,
                    Price = 567
                },
                new Car
                {
                    Name = "Largus",
                    Brand = LADA,
                    Pow = 87,
                    Long = 4470,
                    Price = 558
                },
                new Car
                {
                    Name = "Corolla",
                    Brand = Toyota,
                    Pow = 122,
                    Long = 4300,
                    Price = 1173
                },
                new Car
                {
                    Name = "Camry",
                    Brand = Toyota,
                    Pow = 249,
                    Long = 4500,
                    Price = 1573
                },
                new Car
                {
                    Name = "Hillux",
                    Brand = Toyota,
                    Pow = 177,
                    Long = 4420,
                    Price = 2306
                },
                new Car
                {
                    Name = "Land Cruiser 200",
                    Brand = Toyota,
                    Pow = 309,
                    Long = 5200,
                    Price = 4875
                },
                new Car
                {
                    Name = "Outlander",
                    Brand = Mitsubishi,
                    Pow = 146,
                    Long = 4800,
                    Price = 1449
                },
                new Car
                {
                    Name = "L200",
                    Brand = Mitsubishi,
                    Pow = 154,
                    Long = 5300,
                    Price = 1869
                },
                new Car
                {
                    Name = "Pajero",
                    Brand = Mitsubishi,
                    Pow = 200,
                    Long = 4900,
                    Price = 2719
                },
                new Car
                {
                    Name = "Qashqai",
                    Brand = Nissan,
                    Pow = 115,
                    Long = 4460,
                    Price = 1120
                },
                new Car
                {
                    Name = "X-trail",
                    Brand = Nissan,
                    Pow = 144,
                    Long = 4380,
                    Price = 1370
                },
                new Car
                {
                    Name = "Murano",
                    Brand = Nissan,
                    Pow = 249,
                    Long = 4690,
                    Price = 2099
                },

            };

            context.Cars.AddRange(cars);

            context.SaveChanges();
        }
    }
}
