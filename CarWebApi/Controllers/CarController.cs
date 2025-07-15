using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.CQRS.Queries.Cars;
using CarWebApi.CQRS.QueriesParam;
using CarWebApi.Extensions;
using CarWebApi.Models.Cars;
using CarWebApi.PageParams;
using CarWebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers;

/// <summary>
/// Контроллер для работы с автомобилями
/// </summary>
[ApiController]
[Route("api/[controller]")]
// [ApiExplorerSettings(GroupName = "Car")]
public class CarController(IMapper mapper, ILogger<CarController> logger) : ControllerMediator
{
    /// <summary>
    /// Получить список автомобилей
    /// </summary>
    /// <returns>Список автомобилей</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CarList>> Get()
    {
        logger.LogInformation($"{nameof(Get)} is called");
        var query = new GetCarListQuery
            { };
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
    /// Получить список автомобилей с фильтром по мощности двигателя
    /// </summary>
    /// <returns>Список автомобилей с фильтром по мощности двигателя</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPost("powerQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithPowerDto>>> GetWithPower(GetWithPowerQuery powerQuery)
    {
        logger.LogInformation($"{nameof(GetWithPower)} is called");

        var query = new GetCarListPagedWithPowerQuery()
        {
            Filter = new CarFilterParams().WithPower(powerQuery.minPower, powerQuery.maxPower),
            Pagination = powerQuery.paginationParams,
            Sort = new SortParams() { SortBy = powerQuery.sort }
        };
        try
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(GetWithPower)} {ex.Message}");
            return Problem(ex.Message);
        }
    }

    /// <summary>
    /// Получить список автомобилей с фильтром по стоимости
    /// </summary>
    /// <returns>Список автомобилей с фильтром по стоимости</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPost("priceQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithPriceDto>>> GetWithPrice(GetWithPriceQuery priceQuery)
    {
        logger.LogInformation($"{nameof(GetWithPrice)} is called");
        var query = new GetCarListPagedWithPriceQuery()
        {
            Filter = new CarFilterParams().WithPrice(priceQuery.minPrice, priceQuery.maxPrice),
            Pagination = priceQuery.paginationParams,
            Sort = new SortParams() { SortBy = priceQuery.sort }
        };

        try
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(GetWithPrice)} {ex.Message}");
            return Problem(ex.Message);
        }
    }

    /// <summary>
    /// Получить список автомобилей с фильтром по стоимости и мощности двигателя
    /// </summary>
    /// <returns>Список автомобилей с фильтром по стоимости и мощности двигателя</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPost("detailsQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithDetailsDto>>> GetWithDetails(
        GetWithDetailsQuery detailsQuery)
    {
        logger.LogInformation($"{nameof(GetWithDetails)} is called");
        var query = new GetCarListPagedWithDetailsQuery()
        {
            Filter = new CarFilterParams().WithCustomFilter(car =>
                (car.Price >= detailsQuery.minPrice &&
                 car.Price <= detailsQuery.maxPrice) &&
                (car.Pow >= detailsQuery.minPrice &&
                 car.Pow <= detailsQuery.maxPower)),

            Pagination = detailsQuery.paginationParams,
            Sort = new SortParams() { SortBy = detailsQuery.sort }
        };

        try
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(GetWithDetails)} {ex.Message}");
            return Problem(ex.Message);
        }
    }

    /// <summary>
    /// Получить автомобиль по ИД
    /// </summary>
    /// <param name="id">ИД автомобиля</param>
    /// <returns>автомобиль</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CarDetails>> Get(Guid id)
    {
        logger.LogInformation($"{nameof(Get)} is called. ID: {id}");
        var query = new GetCarDetailsQuery
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
    /// Создать автомобиль
    /// </summary>
    /// <param name="createCarDto">Модель данных автомобиля</param>
    /// <returns>ИД автомобиля</returns>
    /// <response code="201">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCarDto createCarDto)
    {
        logger.LogInformation($"{nameof(Create)} is called");
        
        try
        {
            var command = mapper.Map<CreateCarCommand>(createCarDto);
            var carId = await Mediator.Send(command);
            return Ok(carId);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Create)} {ex.Message}");
            return Problem(ex.Message);
        }
    }
    
    /// <summary>
    /// Обновить автомобиль
    /// </summary>
    /// <param name="updateCarDto">Модель обновления автомобиля от клиента</param>
    /// <returns>Ничего не возвращаем</returns>
    /// <response code="204">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateCarDto updateCarDto)
    {
        logger.LogInformation($"{nameof(Update)} is called. ID: {updateCarDto.Id}");

        try
        {
            var command = mapper.Map<UpdateCarCommand>(updateCarDto);
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
    /// Удалить автомобиль
    /// </summary>
    /// <param name="id">ИД автомобиля</param>
    /// <returns>Ничего не возвращаем</returns>
    /// <response code="204">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation($"{nameof(Delete)} is called. ID: {id}");
        var command = new DeleteCarCommand
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