namespace fast_currencies_be;

public class Subscription : EntityBase
{
    public int MaxRequests { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<User> Users { get; set; }

    public Subscription()
    {
        this.Users = new List<User>();
    }
}
