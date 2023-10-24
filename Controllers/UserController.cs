using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    [Route("mi-profile")]
    public IActionResult MiProfile()
    {
        try {
            var user = _userService.GetUserProfile();
            return Ok(user);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
