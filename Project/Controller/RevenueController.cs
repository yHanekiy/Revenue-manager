using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Service;

namespace Project.Controller;

[ApiController]
[Route("revenue")]
public class RevenueController(IRevenueService revenueService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<OkObjectResult> RevenueCalculation(int idObject, string? currency)
    {
       return Ok(await revenueService.RevenueCalculation(idObject, currency));
    }
    [Authorize]
    [HttpGet("predictable")]
    public async Task<OkObjectResult> PredictableRevenueCalculation(int idObject, string? currency)
    {
        return Ok(await revenueService.PredictableRevenueCalculation(idObject, currency));
    }
}