namespace fast_currencies_be;

public class CurrencyRate : EntityBase
{
    public int CurrencyOriginId { get; set; }
    public int CurrencyDestinationId { get; set; }
    public decimal Rate { get; set; }
    public Currency CurrencyOrigin { get; set; }
    public Currency CurrencyDestination { get; set; }
}
