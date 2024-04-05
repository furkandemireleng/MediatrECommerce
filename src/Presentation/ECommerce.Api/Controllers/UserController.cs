using ECommerce.Application.Features.Auth.Command.Login;
using ECommerce.Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK,response);
    }
}