namespace fast_currencies_be;

public class ConvertionHistoryDto
{
    public DateTime Date { get; set; }
    public string CurrencyFromSymbol { get; set; } = string.Empty;
    public string CurrencyToSymbol { get; set; } = string.Empty;
}
