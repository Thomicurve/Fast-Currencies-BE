﻿using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("register")]
    public IActionResult RegisterUser(RegisterUserDto registerUserDto)
    {
        try {
            _userService.RegisterUser(registerUserDto);
            return Ok();
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Authenticate(LoginUserDto loginUserDto)
    {
        try {
            string jwt = _userService.Authenticate(loginUserDto);
            return Ok(new { token = jwt });
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("mi-perfil")]
    public IActionResult MiProfile()
    {
        try {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value; 

            if(userId is null) throw new Exception("No se pudo obtener el id del usuario");

            var user = _userService.GetUserById(int.Parse(userId));
            return Ok(user);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
