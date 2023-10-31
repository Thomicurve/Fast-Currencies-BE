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
    private readonly SessionService _sessionService;
    public CurrencyController(
        CurrencyService currencyService, 
        EntityRepository<Currency> currencyRepository,
        SessionService sessionService)
    {
        _currencyService = currencyService;
        _currencyRepository = currencyRepository;
        _sessionService = sessionService;
        
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
    public IActionResult Create(CurrencyForCreationDto currencyDto) {
        Role roleClaim = _sessionService.GetUserRol();
        if(roleClaim == Role.User) {
            return Unauthorized("No tienes permisos para realizar esta acción.");
        }

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
        Role roleClaim = _sessionService.GetUserRol();
        if(roleClaim == Role.User) {
            return Unauthorized("No tienes permisos para realizar esta acción.");
        }

        try {

            Currency? currentCurrency = _currencyRepository.GetById(dto.Id);

            if(currentCurrency != null) {
                currentCurrency.IC = dto.IC;
                currentCurrency.Leyend = dto.Leyend;
                currentCurrency.Symbol = dto.Symbol;

                return Ok(_currencyRepository.Update(currentCurrency));
            } else {
                return NotFound("No se encontró la moneda.");
            }

        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        Role roleClaim = _sessionService.GetUserRol();
        if(roleClaim == Role.User) {
            return Unauthorized("No tienes permisos para realizar esta acción.");
        }
        
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
