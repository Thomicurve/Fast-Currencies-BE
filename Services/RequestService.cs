namespace fast_currencies_be;

public class RequestService
{
    private readonly EntityRepository<Request> _requestRepository;
    private readonly EntityRepository<User> _userRepository;
    private readonly EntityRepository<ConvertionHistory> _convertionHistoryRepository;
    private readonly FastCurrenciesAppContext _appContext;

    public RequestService(
        EntityRepository<Request> requestRepository,
        EntityRepository<User> userRepository,
        EntityRepository<ConvertionHistory> convertionHistoryRepository,
        FastCurrenciesAppContext appContext
    )
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _convertionHistoryRepository = convertionHistoryRepository;
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

        if(userRequest.FirstRequestFromMonthDate is null) {
            userRequest.FirstRequestFromMonthDate = DateTime.Now;
        }

        this.VerifyUserRequestsDate();

        if (userRequest.CurrentRequests >= user.Subscription.MaxRequests)
        {
            throw new Exception("Se ha superado el límite de peticiones");
        } else {
            userRequest.CurrentRequests++;
        }
        
        _requestRepository.Update(userRequest);
    }

    public bool VerifyUserRequestsDate()
    {
        int userId = _appContext.UserId!.Value;
        Request userRequest = _requestRepository
            .GetAll()
            .FirstOrDefault(x => x.UserId == userId)!;

        // Si la ultima petición fue en otro mes, se reinicia el contador
        if ((userRequest.FirstRequestFromMonthDate - DateTime.Now) > TimeSpan.FromDays(30))
        {
            userRequest.CurrentRequests = 0;
            userRequest.FirstRequestFromMonthDate = DateTime.Now;
            _requestRepository.Update(userRequest);
            return true;
        }

        return false;
    }
}

