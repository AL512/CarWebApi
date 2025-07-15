using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.CQRS.Queries.Countries;
using CarWebApi.Models.Countries;
using CarWebApi.Queries.Countries;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers;

/// <summary>
/// Контроллер для работы со странами производителям автомобилей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CountriesController(IMapper mapper, ILogger<BrandController> logger) : ControllerMediator
{
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
        logger.LogInformation($"{nameof(Get)} is called");
        var query = new GetCountryListQuery();

        try
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Get)} {ex.Message}");
            return Problem(ex.Message);
        }
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
        logger.LogInformation($"{nameof(Get)} is called");
        var query = new GetCountryDetailsQuery
        {
            Id = id
        };

        try
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Get)} {ex.Message}");
            return Problem(ex.Message);
        }
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
        logger.LogInformation($"{nameof(Create)} is called");
        try
        {
            var command = mapper.Map<CreateCountryCommand>(createCountryDto);
            var countryId = await Mediator.Send(command);
            return Ok(countryId);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Create)} {ex.Message}");
            return Problem(ex.Message);
        }
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
        logger.LogInformation($"{nameof(Update)} is called");
        try
        {
            var command = mapper.Map<UpdateCountryCommand>(updateCountryDto);
            await Mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Update)} {ex.Message}");
            return Problem(ex.Message);
        }
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
        logger.LogInformation($"{nameof(Delete)} is called");
        var command = new DeleteCountryCommand
        {
            Id = id
        };

        try
        {
            await Mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Delete)} {ex.Message}");
            return Problem(ex.Message);
        }
    }
}