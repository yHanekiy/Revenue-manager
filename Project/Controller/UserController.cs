using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Service;

namespace Project.Controller;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterModelDTO model)
    {
        await userService.RegisterUser(model);
        return Ok("User was registered");
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequestDTO loginRequest)
    {
        return Ok(await userService.LoginUser(loginRequest));
    }
}