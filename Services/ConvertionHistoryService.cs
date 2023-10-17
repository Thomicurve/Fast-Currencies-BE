namespace fast_currencies_be;

public class ConvertionHistoryService
{
    private readonly EntityRepository<ConvertionHistory> _convertionHistoryRepository;
    private readonly FastCurrenciesAppContext _appContext;
    public ConvertionHistoryService (
        EntityRepository<ConvertionHistory> convertionHistoryRepository,
        FastCurrenciesAppContext appContext) {
        _convertionHistoryRepository = convertionHistoryRepository;
        _appContext = appContext;
    }

    public ICollection<ConvertionHistoryDto> GetHistory() {
        List<ConvertionHistory> history = _convertionHistoryRepository
            .GetAllIncluding(x => x.CurrencyFrom,
                            x => x.CurrencyTo)
            .Where(x => x.UserId == _appContext.UserId)
            .ToList();
        
        return history.Select(x => new ConvertionHistoryDto {
            Date = x.Date,
            CurrencyFromCode = x.CurrencyFrom.Code,
            CurrencyToCode = x.CurrencyTo.Code
        }).ToList();
    }

    public void RegisterHistory(int fromCurrencyId, int toCurrencyId) {
        _convertionHistoryRepository.Add(new ConvertionHistory {
            UserId = _appContext.UserId!.Value,
            Date = DateTime.Now,
            CurrencyFromId = fromCurrencyId,
            CurrencyToId = toCurrencyId
        });
    }
}
