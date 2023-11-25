namespace fast_currencies_be.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Leyend { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal IC { get; set; }
    }
}
