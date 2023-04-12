using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.CQRS.Queries.Countries;
using CarWebApi.Models.Countries;
using CarWebApi.Queries.Countries;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы со странами производителям автомобилей
    /// </summary>
    public class CountriesController : ControllerMediator
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// Конструктор контроллер работы с марками автомобилей
        /// </summary>
        /// <param name="mapper">Маппер</param>
        public CountriesController(IMapper mapper) => Mapper = mapper;
 
        /// <summary>
        /// Получить список стран производителей
        /// </summary>
        /// <returns>Список стран</returns>
        /// <response code="200">Success</response>
        /// <response code="401">если пользователь не авторизован</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CountryList>>> Get()
        {
            var query = new GetCountryListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Получить страну производителя по ИД
        /// </summary>
        /// <param name="id">ИД страны</param>
        /// <returns>автомобиль</returns>
        /// <response code="200">Success</response>
        /// <response code="401">если пользователь не авторизован</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetails>> Get(Guid id)
        {
            var query = new GetCountryDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Добавить страну производителя
        /// </summary>
        /// <param name="createCarDto">Транспортная модель данных автомобиля</param>
        /// <returns>ИД автомобиля</returns>
        /// <response code="201">Success</response>
        /// <response code="401">если пользователь не авторизован</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Country>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            var command = Mapper. Map<CreateCountryCommand>(createCountryDto);
            var countryId = await Mediator.Send(command);
            return Ok(countryId);
        }

        /// <summary>
        /// Обновить страну производителя
        /// </summary>
        /// <param name="updateCountryDto">Модель обновления стнары производителя от клиента</param>
        /// <returns>Ничего не возвращаем</returns>
        /// <response code="204">Success</response>
        /// <response code="401">если пользователь не авторизован</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateCountryDto updateCountryDto)
        {
            var command = Mapper.Map<UpdateCountryCommand>(updateCountryDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Удалить страну производителя
        /// </summary>
        /// <param name="id">ИД страны производителя</param>
        /// <returns>Ничего не возвращаем</returns>
        /// <response code="204">Success</response>
        /// <response code="401">если пользователь не авторизован</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Country>> Delete(Guid id)
        {
            var command = new DeleteCountryCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
