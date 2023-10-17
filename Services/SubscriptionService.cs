namespace fast_currencies_be;

public class SubscriptionService
{
    private readonly EntityRepository<Subscription> _subscriptionRepository;
    private readonly EntityRepository<User> _userRepository;
    private readonly FastCurrenciesAppContext _appContext;
    public SubscriptionService(
        EntityRepository<Subscription> subscriptionRepository,
        EntityRepository<User> userRepository,
        FastCurrenciesAppContext appContext)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _appContext = appContext;
    }
    
    public void UpdateSubscription(int subscripcionId) {
        User user = _userRepository.GetById(_appContext.UserId!.Value)!;
        Subscription? subscription = _subscriptionRepository.GetById(subscripcionId);

        if (subscription is null) {
            throw new Exception("No se encontró el tipo de suscripción");
        }

        user.SubscriptionId = subscription.Id;
        _userRepository.Update(user);
    }
}
