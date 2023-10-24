namespace fast_currencies_be;

public enum Role
{
    Admin,
    User
}

public class User : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public Request Request { get; set; }
    public List<ConvertionHistory> ConvertionHistories { get; set; }
}
