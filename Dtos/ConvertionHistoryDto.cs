namespace fast_currencies_be;

public class ConvertionHistoryDto
{
    public DateTime Date { get; set; }
    public string CurrencyFromCode { get; set; } = string.Empty;
    public string CurrencyToCode { get; set; } = string.Empty;
}
