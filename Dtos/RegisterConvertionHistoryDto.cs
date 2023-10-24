namespace fast_currencies_be.Dtos
{
    public class RegisterConvertionHistoryDto
    {
        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }
        public decimal PriceToConvert { get; set; }
        public decimal ConvertedPrice { get; set; }
    }
}
