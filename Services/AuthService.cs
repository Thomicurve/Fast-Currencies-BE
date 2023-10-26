using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fast_currencies_be;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly EntityRepository<User> _userRepository;
    private readonly EntityRepository<Subscription> _subscriptionRepository;
    private readonly EntityRepository<Request> _requestRepository;
    public AuthService(
        IConfiguration configuration,
        EntityRepository<User> userRepository,
        EntityRepository<Subscription> subscriptionRepository,
        EntityRepository<Request> requestRepository) {
        _configuration = configuration;
        _userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
        _requestRepository = requestRepository;
    }

    
    public void RegisterUser(RegisterUserDto registerUserDto)
    {
        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
        {
            throw new Exception("Las contraseñas no coinciden");
        }

        if (_userRepository.GetAll().Any(x => x.Email == registerUserDto.Email))
        {
            throw new Exception("Ya existe un usuario con ese email");
        }

        registerUserDto.Password = HashPassword(registerUserDto.Password);

        Subscription? subscriptionFree = _subscriptionRepository
            .GetAll()
            .FirstOrDefault(s => s.Description == "Free");

        if (subscriptionFree is null)
        {
            throw new Exception("No se encontró la suscripción Gratis");
        }

        User userCreated = _userRepository.Add(new User
        {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password,
            SubscriptionId = subscriptionFree.Id,
            Role = Role.User
        });

        _requestRepository.Add(new Request
        {
            UserId = userCreated.Id,
            FirstRequestFromMonthDate = null,
            CurrentRequests = 0
        });
    }
    public string? Authenticate(LoginUserDto loginUserDto)
    {
        User? user = _userRepository
            .GetAll()
            .FirstOrDefault(x => x.Email == loginUserDto.Email);

        if (user is null)
        {
            return null;
        }

        bool isPasswordRight = this.VerifyPassword(loginUserDto.Password, user.Password);
        if (!isPasswordRight)
        {
            return null;
        }

        return this.GenerateJwtToken(user);
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
            new Claim("rol", user.Role.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return jwt;
    }
}
