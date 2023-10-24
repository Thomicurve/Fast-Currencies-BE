namespace fast_currencies_be;

public class ConvertionHistory : EntityBase
{
    public DateTime Date { get; set; }
    public decimal PriceToConvert { get; set; }
    public decimal ConvertedPrice { get; set; }
    public int CurrencyFromId { get; set; }
    public int CurrencyToId { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
    public Currency CurrencyTo { get; set; }
    public Currency CurrencyFrom { get; set; }
}
