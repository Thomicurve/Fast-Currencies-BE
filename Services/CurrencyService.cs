namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    public CurrencyService(RequestService requestService)
    {
        _requestService = requestService;
    }

    public decimal Convert(decimal amount) {
        _requestService.IncrementRequestsCount();
        return amount * 1.2m;
    }
}
