using System.ComponentModel.DataAnnotations;

namespace fast_currencies_be;

public class UpdateSubscriptionDto
{
    [Required]
    public int SubscriptionId { get; set; }
}
