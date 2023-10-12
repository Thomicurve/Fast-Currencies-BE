namespace fast_currencies_be;

public class User : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public Request Request { get; set; }
}
