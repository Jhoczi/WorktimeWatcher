using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WorktimeWatcherApi.Models;
using WorktimeWatcherApi.Services;

namespace WorktimeWatcherApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly IConfiguration _config;
    private readonly UserService _userService;

    public LoginController(IConfiguration config, UserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User login)
    {
        IActionResult response = Unauthorized();
        var user = await AuthenticateUser(login);

        if (user == null) return response;
        var tokenString = GenerateJsonWebToken(user);
        response = Ok(new {token = tokenString});

        return response;
    }

    private string GenerateJsonWebToken(User userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<User?> AuthenticateUser(User user)
    {
        var userList = await _userService.GetAsync();
        var userResult = userList.Find(x => x.Login == user.Login && x.Password == user.Password);
        return userResult ?? null;
    }
}