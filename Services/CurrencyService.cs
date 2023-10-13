namespace fast_currencies_be;

public class CurrencyService
{
    private readonly RequestService _requestService;
    public CurrencyService(RequestService requestService)
    {
        _requestService = requestService;
    }

    public decimal Convert(decimal amount, int userId) {
        _requestService.IncrementRequestsCount(userId);
        return amount * 1.2m;
    }
}
