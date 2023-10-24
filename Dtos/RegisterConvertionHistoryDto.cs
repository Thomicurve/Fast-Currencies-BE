using System.ComponentModel.DataAnnotations;

namespace fast_currencies_be.Dtos
{
    public class RegisterConvertionHistoryDto
    {
        [Required]
        public int CurrencyFromId { get; set; }
        [Required]
        public int CurrencyToId { get; set; }
        [Required]
        public decimal PriceToConvert { get; set; }
        [Required]
        public decimal ConvertedPrice { get; set; }
    }
}
