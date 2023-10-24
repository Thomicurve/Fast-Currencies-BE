using System.ComponentModel.DataAnnotations;

namespace fast_currencies_be;

public class ConvertCurrencyDto
{
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public int FromCurrencyId { get; set; }
    [Required]
    public int ToCurrencyId { get; set; }
}
