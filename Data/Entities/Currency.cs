namespace fast_currencies_be;

public class Currency : EntityBase
{
    public string Leyend {get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal IC { get; set; }
}
