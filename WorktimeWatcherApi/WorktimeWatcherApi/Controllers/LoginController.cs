using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorktimeWatcherApi.Models;
using WorktimeWatcherApi.Services;

namespace WorktimeWatcherApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly UserService _userService;
    private readonly ExtensionsService _extensionsService;

    public LoginController(UserService userService, ExtensionsService extensionsService)
    {
        _userService = userService;
        _extensionsService = extensionsService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User login)
    {
        IActionResult response = Unauthorized();
        var user = await _userService.AuthenticateUser(login);

        if (user == null) return response;
        var tokenString = _extensionsService.GenerateJsonWebToken(user);
        response = Ok(new {token = tokenString});

        return response;
    }
}