using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.CQRS.Queries.Brands;
using CarWebApi.Models.Brands;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers;

/// <summary>
/// Контроллер для работы с марками автомобилей
/// </summary>
public class BrandController(IMapper mapper, ILogger<BrandController> logger) : ControllerMediator
{
    /// <summary>
    /// Получить список марок автомобилей
    /// </summary>
    /// <returns>Список марок автомобилей</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BrandList>> Get()
    {
        logger.LogInformation($"{nameof(Get)} is called");
        var query = new GetBrandListQuery
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
    /// Получить марку автомобиля по ИД
    /// </summary>
    /// <param name="id">ИД марки автомобиля</param>
    /// <returns>Марка автомобиля</returns>
    /// <response code="200">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BrandDetails>> Get(Guid id)
    {
        logger.LogInformation($"{nameof(Get)} is called. ID: {id}");
        var query = new GetBrandDetailsQuery
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
    /// Создать марку автомобиля
    /// </summary>
    /// <param name="createBrandDto">Модель данных марки автомобиля</param>
    /// <returns>ИД марки автомобиля</returns>
    /// <response code="201">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBrandDto createBrandDto)
    {
        logger.LogInformation($"{nameof(Create)} is called");

        try
        {
            var command = mapper.Map<CreateBrandCommand>(createBrandDto);
            var brandId = await Mediator.Send(command);
            return Ok(brandId);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(Create)} {ex.Message}");
            return Problem(ex.Message);
        }
    }
    
    /// <summary>
    /// Обновить марку автомобиля
    /// </summary>
    /// <param name="updateBrandDto">Модель обновления марки автомобиля от клиента</param>
    /// <returns>Ничего не возвращаем</returns>
    /// <response code="204">Success</response>
    /// <response code="401">если пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateBrandDto updateBrandDto)
    {
        logger.LogInformation($"{nameof(Update)} is called. ID: {updateBrandDto.Id}");
        try
        {
            var command = mapper.Map<UpdateBrandCommand>(updateBrandDto);
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
    /// Удалить марку автомобиля
    /// </summary>
    /// <param name="id">ИД марки автомобиля</param>
    /// <returns>Ничего не возвращаем</returns>
    /// <response code="204">Success</response>
    /// <response code="401">если пользователь не авторизован</response>

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        logger.LogInformation($"{nameof(Delete)} is called. ID: {id}");
        var command = new DeleteBrandCommand
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