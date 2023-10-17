﻿namespace fast_currencies_be;

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

        // Si la ultima petición fue en otro mes, se reinicia el contador
        if (userRequest.LastRequestMonth != DateTime.Now.Month)
        {
            userRequest.CurrentRequests = 1;
            userRequest.LastRequestMonth = DateTime.Now.Month;
        }

        if (userRequest.CurrentRequests >= user.Subscription.MaxRequests)
        {
            throw new Exception("Se ha superado el límite de peticiones");
        } else {
            userRequest.CurrentRequests++;
        }
        
        _requestRepository.Update(userRequest);
    }
}

