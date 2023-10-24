using fast_currencies_be.Dtos;

namespace fast_currencies_be;

public class ConvertionHistoryService
{
    private readonly EntityRepository<ConvertionHistory> _convertionHistoryRepository;
    private readonly SessionService _sessionService;
    public ConvertionHistoryService (
        EntityRepository<ConvertionHistory> convertionHistoryRepository,
        SessionService sessionService) {
        _convertionHistoryRepository = convertionHistoryRepository;
        _sessionService = sessionService;
    }

    public ICollection<ConvertionHistoryDto> GetHistory() {
        int userId = _sessionService.GetUserId();
        List<ConvertionHistory> history = _convertionHistoryRepository
            .GetAllIncluding(x => x.CurrencyFrom,
                            x => x.CurrencyTo)
            .Where(x => x.UserId == userId)
            .ToList();
        
        return history.Select(x => new ConvertionHistoryDto {
            Date = x.Date,
            CurrencyFromSymbol = x.CurrencyFrom.Symbol,
            CurrencyToSymbol = x.CurrencyTo.Symbol,
            PriceToConvert = x.PriceToConvert,
            ConvertedPrice = x.ConvertedPrice
        }).ToList();
    }

    public void RegisterHistory(RegisterConvertionHistoryDto dto) {
        int userId = _sessionService.GetUserId();
        _convertionHistoryRepository.Add(new ConvertionHistory {
            UserId = userId,
            Date = DateTime.Now,
            CurrencyFromId = dto.CurrencyFromId,
            CurrencyToId = dto.CurrencyToId,
            PriceToConvert = dto.PriceToConvert,
            ConvertedPrice = dto.ConvertedPrice
        });
    }
}
