using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;
    private readonly EntityRepository<Currency> _currencyRepository;
    public CurrencyController(
        CurrencyService currencyService, 
        EntityRepository<Currency> currencyRepository)
    {
        _currencyService = currencyService;
        _currencyRepository = currencyRepository;
        
    }

    [HttpGet]
    public IActionResult GetAll() {
        try {
            return Ok(_currencyRepository.GetAll());
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) {
        try {
            return Ok(_currencyRepository.GetById(id));
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult GetById(CurrencyForCreationDto currencyDto) {
        try {
            Currency currency = new Currency {
                Leyend = currencyDto.Leyend,
                Symbol = currencyDto.Symbol,
                IC = currencyDto.IC
            };
            return Ok(_currencyRepository.Add(currency));
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update(CurrencyForCreationDto dto) {
        try {
            Currency currency = new Currency {
                Id = dto.Id,
                Leyend = dto.Leyend,
                Symbol = dto.Symbol,
                IC = dto.IC
            };
            return Ok(_currencyRepository.Update(currency));
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        try {
            return Ok(_currencyRepository.Delete(id));
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("convert")]
    public IActionResult Convert(ConvertCurrencyDto convertCurrencyDto)
    {
        try {
            decimal result = _currencyService.Convert(convertCurrencyDto);
            return Ok(new { value = result});
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}
