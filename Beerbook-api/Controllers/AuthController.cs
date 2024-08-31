using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/auth")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await _authService.CheckUserExist(request.Email))
        {
            return BadRequest("User with this email already exist");
        }

        await _authService.Register(request);
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Login(request);
        if (token == null)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(new { Token = token});
    }

}