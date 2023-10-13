using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;
    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpPost("convertir")]
    public IActionResult Convert(ConvertCurrencyDto convertCurrencyDto)
    {
        try {
            string userId = User.Claims.FirstOrDefault(x => x.Type == "userId")!.Value;
            return Ok(_currencyService.Convert(convertCurrencyDto.Amount, int.Parse(userId)));
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}
