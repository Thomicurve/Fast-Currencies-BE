using System.ComponentModel.DataAnnotations;

namespace fast_currencies_be;

public class CurrencyForCreationDto
{
    public int Id { get; set; }
    [Required]
    public string Leyend {get; set; } = string.Empty;
    [Required]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    public decimal IC { get; set; }
}
