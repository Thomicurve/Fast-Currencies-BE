using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fast_currencies_be;

public class UserService
{
    private readonly EntityRepository<User> _userRepository;
    private readonly IConfiguration _configuration;
    public UserService(EntityRepository<User> userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public void RegisterUser(RegisterUserDto registerUserDto)
    {
        if(registerUserDto.Password != registerUserDto.ConfirmPassword)
        {
            throw new Exception("Las contraseñas no coinciden");
        }

        if(_userRepository.GetAll().Any(x => x.Email == registerUserDto.Email))
        {
            throw new Exception("Ya existe un usuario con ese email");
        }

        registerUserDto.Password = HashPassword(registerUserDto.Password);

        _userRepository.Add(new User {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password
        });
    }

    public string Authenticate(LoginUserDto loginUserDto) {
        User? user = _userRepository
            .GetAll()
            .FirstOrDefault(x => x.Email == loginUserDto.Email);

        if(user is null) {
            throw new Exception("El email o la contraseña son incorrectos");
        }
            
        bool isPasswordRight = this.VerifyPassword(loginUserDto.Password, user.Password);
        if(!isPasswordRight) {
            throw new Exception("El email o la contraseña son incorrectos");
        }

        return this.GenerateJwtToken(user);
    }
    public UserDto GetUserById(int id)
    {
        User entity = _userRepository.GetById(id);
        return new UserDto {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email
        };
    }
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    private bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(720),
        signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return jwt;
    }
}
