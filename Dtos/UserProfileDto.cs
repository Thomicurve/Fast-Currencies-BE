namespace fast_currencies_be;

public class UserProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SubscriptionDescription { get; set; } = string.Empty;
    public int RequestsRemaining { get; set; }
    public string RolDescription { get; set; } = string.Empty;
}
