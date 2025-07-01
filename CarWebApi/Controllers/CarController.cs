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
// [ApiExplorerSettings(GroupName = "Car")]
public class CarController(IMapper mapper) : ControllerMediator
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
        var query = new GetCarListQuery
            { };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost("powerQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithPowerDto>>> GetWithPower(GetWithPowerQuery powerQuery)
    {
        var query = new GetCarListPagedWithPowerQuery()
        {
            Filter = new CarFilterParams().WithPower(powerQuery.minPower, powerQuery.maxPower),
            Pagination = powerQuery.paginationParams,
            Sort = new SortParams() { SortBy = powerQuery.sort }
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost("priceQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithPriceDto>>> GetWithPrice(GetWithPriceQuery priceQuery)
    {
        var query = new GetCarListPagedWithPriceQuery()
        {
            Filter = new CarFilterParams().WithPrice(priceQuery.minPrice, priceQuery.maxPrice),
            Pagination = priceQuery.paginationParams,
            Sort = new SortParams() { SortBy = priceQuery.sort }
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost("detailsQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedRequest<CarLookupWithDetailsDto>>> GetWithDetails(
        GetWithDetailsQuery detailsQuery)
    {
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

        var vm = await Mediator.Send(query);
        return Ok(vm);
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
        var query = new GetCarDetailsQuery
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
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
        var command = mapper.Map<CreateCarCommand>(createCarDto);
        var carId = await Mediator.Send(command);
        return Ok(carId);
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
        var command = mapper.Map<UpdateCarCommand>(updateCarDto);
        await Mediator.Send(command);
        return NoContent();
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
        var command = new DeleteCarCommand
        {
            Id = id
        };
        await Mediator.Send(command);
        return NoContent();
    }
}