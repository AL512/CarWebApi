using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ControllerMediator : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}