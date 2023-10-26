namespace fast_currencies_be;

public class UserService
{
    private readonly EntityRepository<User> _userRepository;
    private readonly EntityRepository<Request> _requestRepository;
    private readonly RequestService _requestService;
    private readonly SessionService _sessionService;
    public UserService(
        EntityRepository<User> userRepository,
        EntityRepository<Request> requestRepository,
        RequestService requestService,
        SessionService sessionService)
    {
        _userRepository = userRepository;
        _requestRepository = requestRepository;
        _requestService = requestService;
        _sessionService = sessionService;
    }

    public UserProfileDto GetUserProfile()
    {
        int userId = _sessionService.GetUserId();

        User? user = _userRepository
            .GetAllIncluding(x => x.Subscription)
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            throw new Exception("No se encontró el usuario");
        }

        // Antes de obtener las requests valido si se paso el mes, si es así, reseteo las requests
        _requestService.VerifyUserRequestsDate();
        Request userRequests = _requestRepository
            .GetAll()
            .FirstOrDefault(x => x.UserId == userId)!;

        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            SubscriptionDescription = user.Subscription.Description,
            RequestsRemaining = user.Subscription.MaxRequests - userRequests.CurrentRequests,
            RolDescription = user.Role.ToString()
        };
    }
}
