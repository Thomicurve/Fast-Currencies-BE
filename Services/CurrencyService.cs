namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    private readonly EntityRepository<CurrencyRate> _currencyRateRepository;
    public CurrencyService(
        RequestService requestService, 
        EntityRepository<CurrencyRate> currencyRateRepository)
    {
        _requestService = requestService;
        _currencyRateRepository = currencyRateRepository;
    }

    public decimal Convert(ConvertCurrencyDto dto) {
        var rate = _currencyRateRepository
            .GetAll()
            .FirstOrDefault(r => r.CurrencyDestinationId == dto.ToCurrencyId && 
                            r.CurrencyOriginId == dto.FromCurrencyId);
        if(rate is null) {
            throw new Exception("No se encontró una tasa de conversión para las monedas seleccionadas");
        }

        _requestService.IncrementRequestsCount();
        return dto.Amount * rate.Rate;
    }
}
