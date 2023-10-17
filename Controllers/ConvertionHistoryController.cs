using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ConvertionHistoryController : ControllerBase
{
    private readonly ConvertionHistoryService _convertionHistoryService;
    public ConvertionHistoryController(ConvertionHistoryService convertionHistoryService)
    {
        _convertionHistoryService = convertionHistoryService;
    }

    [HttpGet]
    public IActionResult GetHistory() {
        try {
            return Ok(_convertionHistoryService.GetHistory());
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
