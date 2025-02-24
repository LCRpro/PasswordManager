using Microsoft.AspNetCore.Mvc;
using PasswordManager.Api.Services;
using PasswordManager.Core.Models;

namespace PasswordManager.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var createdUser = await _authService.Register(user.Username, user.PasswordHash);
        if (createdUser == null)
            return BadRequest("Nom d'utilisateur déjà pris.");

        return Ok(new { message = "Inscription réussie" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var token = await _authService.Login(user.Username, user.PasswordHash);
        if (token == null)
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");

        return Ok(new { token });
    }
}
