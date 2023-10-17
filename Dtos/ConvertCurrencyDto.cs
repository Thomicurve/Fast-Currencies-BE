namespace fast_currencies_be;

public class ConvertCurrencyDto
{
    public decimal Amount { get; set; }
    public int FromCurrencyId { get; set; }
    public int ToCurrencyId { get; set; }
}
