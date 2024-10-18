using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Service;

namespace Project.Controller;

[ApiController]
[Route("subscribes")]
public class SubscribeController(ISubscriptionService subscriptionService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeAddDTO subscribeAddDto)
    {
        await subscriptionService.Subscribe(subscribeAddDto);
        return Ok("Subscription was completed");
    }

    [Authorize]
    [HttpPatch("{idSubscription:int}")]
    public async Task<IActionResult> PayForSubscription(int idSubscription, PaymentDTO paymentDto)
    {
        await subscriptionService.PayForSubscription(idSubscription, paymentDto);
        return Ok("Payment was made");
    }
}