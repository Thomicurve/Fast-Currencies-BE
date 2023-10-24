﻿using System.ComponentModel.DataAnnotations;

namespace fast_currencies_be;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
