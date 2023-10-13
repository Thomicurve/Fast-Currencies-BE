using System.Security.Claims;

namespace fast_currencies_be;

public class RequestService
{
    private readonly EntityRepository<Request> _requestRepository;
    private readonly EntityRepository<User> _userRepository;

    public RequestService(
        EntityRepository<Request> requestRepository,
        EntityRepository<User> userRepository
    )
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
    }

    public void IncrementRequestsCount(int userId)
    {
        Request userRequest = _requestRepository
            .GetAll()
            .FirstOrDefault(x => x.UserId == userId)!;

        User user = _userRepository
            .GetAllIncluding(x => x.Subscription)
            .FirstOrDefault(x => x.Id == userId)!;

        if (userRequest.CurrentRequests >= user.Subscription.MaxRequests)
        {
            throw new Exception("Se ha superado el límite de peticiones");
        }

        userRequest.CurrentRequests++;
        _requestRepository.Update(userRequest);
    }
}

