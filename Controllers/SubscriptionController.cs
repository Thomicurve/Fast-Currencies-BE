using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fast_currencies_be;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService;
    public SubscriptionController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }
    
    [HttpPost]
    [Route("update-subscription")]
    public IActionResult UpdateSubscription([FromBody] UpdateSubscriptionDto dto) {
        try {
            _subscriptionService.UpdateSubscription(dto.SubscriptionId);
            return Ok("Suscripción actualizada");
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetSubscriptions() {
        try {
            return Ok(_subscriptionService.GetSubscriptions());
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
