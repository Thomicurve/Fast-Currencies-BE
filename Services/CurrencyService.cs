namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    private readonly ConvertionHistoryService _convertionHistoryService;
    private readonly EntityRepository<Currency> _currencyRepository;

    public CurrencyService(
        RequestService requestService, 
        ConvertionHistoryService convertionHistoryService,
        EntityRepository<Currency> currencyRepository)
    {
        _requestService = requestService;
        _convertionHistoryService = convertionHistoryService;
        _currencyRepository = currencyRepository;
    }

    public decimal Convert(ConvertCurrencyDto dto) {
        _requestService.IncrementRequestsCount();
        Currency? currencyFromConvert = _currencyRepository.GetById(dto.FromCurrencyId);
        Currency? currencyToConvert = _currencyRepository.GetById(dto.ToCurrencyId);

        if (currencyToConvert is null || currencyFromConvert is null)
        {
            throw new Exception("Currency not found");
        }

        return (dto.Amount * currencyFromConvert.IC) / currencyToConvert.IC;
    }
}
