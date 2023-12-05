using Microsoft.EntityFrameworkCore;

namespace fast_currencies_be;

public class SubscriptionService
{
    private readonly EntityRepository<Subscription> _subscriptionRepository;
    private readonly EntityRepository<User> _userRepository;
    private readonly SessionService _sessionService;
    public SubscriptionService(
        EntityRepository<Subscription> subscriptionRepository,
        EntityRepository<User> userRepository,
        SessionService sessionService)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _sessionService = sessionService;
    }
    
    public List<SubscriptionDto> GetSubscriptions() {
        List<SubscriptionDto> subscriptions = _subscriptionRepository.GetAll()
            .Select(subscription => new SubscriptionDto {
                Id = subscription.Id,
                Name = subscription.Description,
                Price = subscription.Price,
                MaxRequestsPerMonth = subscription.MaxRequests
            }).ToList();

        return subscriptions;
    }
    public void UpdateSubscription(int subscripcionId) {
        int userId = _sessionService.GetUserId();
        User? user = _userRepository
            .GetAllIncluding(x => x.Request)
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            throw new Exception("No se encontró el usuario");
        }

        Subscription? subscription = _subscriptionRepository.GetById(subscripcionId);

        if (subscription is null) {
            throw new Exception("No se encontró el tipo de suscripción");
        }

        user.SubscriptionId = subscription.Id;
        user.Request.CurrentRequests = 0;
        _userRepository.Update(user);
    }
}
