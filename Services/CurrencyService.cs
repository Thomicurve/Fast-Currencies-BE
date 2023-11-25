using fast_currencies_be.Dtos;

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

    public IEnumerable<CurrencyDto> GetAll()
    {
        return _currencyRepository.GetAll().Select(c => new CurrencyDto
        {
            Id = c.Id,
            Leyend = c.Leyend,
            Symbol = c.Symbol,
            IC = c.IC
        });
    }

    public bool Update(CurrencyForCreationDto dto)
    {
        Currency? currentCurrency = _currencyRepository.GetById(dto.Id);

        if (currentCurrency != null)
        {
            currentCurrency.IC = dto.IC;
            currentCurrency.Leyend = dto.Leyend;
            currentCurrency.Symbol = dto.Symbol;
            return _currencyRepository.Update(currentCurrency);
        }
        
        return false;
        
    }

    public bool Create(CurrencyForCreationDto currencyDto)
    {
        Currency currency = new Currency
        {
            Leyend = currencyDto.Leyend,
            Symbol = currencyDto.Symbol,
            IC = currencyDto.IC
        };
        return _currencyRepository.Add(currency) != null;
    }
    public decimal Convert(ConvertCurrencyDto dto) {
        _requestService.IncrementRequestsCount();
        Currency? currencyFromConvert = _currencyRepository.GetById(dto.FromCurrencyId);
        Currency? currencyToConvert = _currencyRepository.GetById(dto.ToCurrencyId);

        if (currencyToConvert is null || currencyFromConvert is null)
        {
            throw new Exception("Currency not found");
        }


        decimal result = (dto.Amount * currencyFromConvert.IC) / currencyToConvert.IC;

        // Registering convertion history
        RegisterConvertionHistoryDto convertionHistoryDto = new RegisterConvertionHistoryDto
        {
            CurrencyFromId = dto.FromCurrencyId,
            CurrencyToId = dto.ToCurrencyId,
            PriceToConvert = dto.Amount,
            ConvertedPrice = result
        };
        _convertionHistoryService.RegisterHistory(convertionHistoryDto);
        return result;
    }
}
