using ManageStudent.Auth.GetUser;
using ManageStudent.Services.AuthPermission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageStudent.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetUser(int id)
    {
        return Ok(await mediator.Send(new GetUserQuery(id)));
    }
}
