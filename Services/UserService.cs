using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fast_currencies_be;

public class UserService
{
    private readonly EntityRepository<User> _userRepository;
    private readonly EntityRepository<Subscription> _subscriptionRepository;
    private readonly EntityRepository<Request> _requestRepository;
    private readonly FastCurrenciesAppContext _appContext;
    private readonly IConfiguration _configuration;
    public UserService(
        EntityRepository<User> userRepository,
        EntityRepository<Subscription> subscriptionRepository,
        EntityRepository<Request> requestRepository,
        FastCurrenciesAppContext appContext,
        IConfiguration configuration)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _configuration = configuration;
        _requestRepository = requestRepository;
        _appContext = appContext;
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

        _userRepository.Add(new User
        {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password,
            SubscriptionId = subscriptionFree.Id
        });

        _requestRepository.Add(new Request
        {
            UserId = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Email == registerUserDto.Email)!.Id,
            CurrentRequests = 0
        });
    }
    public string Authenticate(LoginUserDto loginUserDto)
    {
        User? user = _userRepository
            .GetAll()
            .FirstOrDefault(x => x.Email == loginUserDto.Email);

        if (user is null)
        {
            throw new Exception("El email o la contraseña son incorrectos");
        }

        bool isPasswordRight = this.VerifyPassword(loginUserDto.Password, user.Password);
        if (!isPasswordRight)
        {
            throw new Exception("El email o la contraseña son incorrectos");
        }

        return this.GenerateJwtToken(user);
    }
    public UserProfileDto GetUserProfile()
    {
        int userId = _appContext.UserId!.Value;
        User? user = _userRepository
            .GetAllIncluding(x => x.Subscription)
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            throw new Exception("No se encontró el usuario");
        }

        Request userRequests = _requestRepository
            .GetAll()
            .FirstOrDefault(x => x.UserId == userId)!;

        
        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            SubscriptionDescription = user.Subscription.Description,
            RequestsRemaining = user.Subscription.MaxRequests - userRequests.CurrentRequests
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
