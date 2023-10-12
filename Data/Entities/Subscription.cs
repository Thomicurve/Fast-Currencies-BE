namespace fast_currencies_be;

public class Subscription : EntityBase
{
    public int MaxRequests { get; set; }
    public int CurrentRequests { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
