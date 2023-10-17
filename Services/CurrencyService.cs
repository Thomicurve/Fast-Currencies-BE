namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    private readonly EntityRepository<CurrencyRate> _currencyRateRepository;
    private readonly ConvertionHistoryService _convertionHistoryService;
    public CurrencyService(
        RequestService requestService, 
        EntityRepository<CurrencyRate> currencyRateRepository,
        ConvertionHistoryService convertionHistoryService)
    {
        _requestService = requestService;
        _currencyRateRepository = currencyRateRepository;
        _convertionHistoryService = convertionHistoryService;
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
        _convertionHistoryService.RegisterHistory(dto.FromCurrencyId, dto.ToCurrencyId);
        return dto.Amount * rate.Rate;
    }
}
