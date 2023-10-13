namespace fast_currencies_be;

public class RequestService
{
    private readonly EntityRepository<Request> _requestRepository;
    private readonly EntityRepository<User> _userRepository;
    private readonly FastCurrenciesAppContext _appContext;

    public RequestService(
        EntityRepository<Request> requestRepository,
        EntityRepository<User> userRepository,
        FastCurrenciesAppContext appContext
    )
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _appContext = appContext;
    }

    public void IncrementRequestsCount()
    {
        int userId = _appContext.UserId!.Value;
        Request userRequest = _requestRepository
            .GetAll()
            .FirstOrDefault(x => x.UserId == userId)!;

        User user = _userRepository
            .GetAllIncluding(x => x.Subscription)
            .FirstOrDefault(x => x.Id == userId)!;

        if (userRequest.LastRequestMonth != DateTime.Now.Month)
        {
            userRequest.CurrentRequests = 1;
            userRequest.LastRequestMonth = DateTime.Now.Month;
        } else {
            userRequest.CurrentRequests++;
        }

        if (userRequest.CurrentRequests >= user.Subscription.MaxRequests)
        {
            throw new Exception("Se ha superado el límite de peticiones");
        }

        _requestRepository.Update(userRequest);
    }
}

