﻿namespace fast_currencies_be;

public class RequestService
{
    private readonly EntityRepository<Request> _requestRepository;
    private readonly EntityRepository<User> _userRepository;
    private readonly SessionService _sessionService;
    public RequestService(
        EntityRepository<Request> requestRepository,
        EntityRepository<User> userRepository,
        SessionService sessionService
    )
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _sessionService = sessionService;
    }

    public void IncrementRequestsCount()
    {

        int userId = _sessionService.GetUserId();
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

        if(user.Subscription.MaxRequests == 0) {
            userRequest.CurrentRequests++;
            _requestRepository.Update(userRequest);
            return;
        }

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
        int userId = _sessionService.GetUserId();
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

