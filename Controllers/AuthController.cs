using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService) {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser(RegisterUserDto registerUserDto)
    {
        try {
            _authService.RegisterUser(registerUserDto);
            return Ok();
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Authenticate(LoginUserDto loginUserDto)
    {
        try {
            string? jwt = _authService.Authenticate(loginUserDto);
            
            if(jwt is null) {
                return Unauthorized();
            }

            return Ok(new { token = jwt });
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("role-admin")]
    public IActionResult GetUserRoleIsAdmin()
    {
        bool result = _authService.UserIsAdmin();
        return Ok(result);
    }

}
