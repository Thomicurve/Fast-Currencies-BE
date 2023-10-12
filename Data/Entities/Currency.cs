namespace fast_currencies_be;

public class Currency : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
