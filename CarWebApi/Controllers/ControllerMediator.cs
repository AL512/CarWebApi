using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers
{
    /// <summary>
    /// Контроллер с медиатором
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ControllerMediator : ControllerBase
    {
        /// <summary>
        /// Медиатор
        /// </summary>
        private IMediator _mediator;
        /// <summary>
        /// Медиатор для формирования команд при выполнении запросов
        /// </summary>
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
