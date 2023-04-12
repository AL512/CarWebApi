using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.CQRS.Queries.Brands;
using CarWebApi.Models.Brands;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с марками автомобилей
    /// </summary>
    public class BrandController : ControllerMediator
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// Конструктор контроллер работы с марками автомобилей
        /// </summary>
        /// <param name="mapper">Маппер</param>
        public BrandController(IMapper mapper) => Mapper =  mapper;

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
            var query = new GetBrandListQuery
            { };
            var vm = await Mediator.Send(query);
            return Ok(vm);
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
            var query = new GetBrandDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Создать марки автомобиля
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
            var command = Mapper.Map<CreateBrandCommand>(createBrandDto);
            var brandId = await Mediator.Send(command);
            return Ok(brandId);
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
            var command = Mapper.Map<UpdateBrandCommand>(updateBrandDto);
            await Mediator.Send(command);
            return NoContent();
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
        public  async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBrandCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
