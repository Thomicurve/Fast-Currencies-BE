namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    private readonly ConvertionHistoryService _convertionHistoryService;
    public CurrencyService(
        RequestService requestService, 
        ConvertionHistoryService convertionHistoryService)
    {
        _requestService = requestService;
        _convertionHistoryService = convertionHistoryService;
    }

    public decimal Convert(ConvertCurrencyDto dto) {
        _requestService.IncrementRequestsCount();
        _convertionHistoryService.RegisterHistory(dto.FromCurrencyId, dto.ToCurrencyId);
        return dto.Amount * 2;
    }
}
