using ManageStudent.Auth.SignIn;
using ManageStudent.Auth.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManageStudent.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("Registration")]
    public async Task<IActionResult> Registration([FromBody] SignUpRequestDto command)
    {
        return Ok(await mediator.Send(new SignUpCommand(command)));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] SignInRequestDto command)
    {
        return Ok(await mediator.Send(new SignInCommand(command)));
    }

}
