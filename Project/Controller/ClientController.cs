using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Service;

namespace Project.Controller;

[ApiController]
[Route("clients")]
public class ClientController(IClientService clientService) : ControllerBase
{
    [Authorize]
    [HttpPost("physical")]
    public async Task<IActionResult> AddClientPhysicalToBase(AddClientPhysicalDTO clientPhysicalDto)
    {
        await clientService.AddClientPhysicalToBase(clientPhysicalDto);
        return Ok("Physical client was added");
    }
    [Authorize]
    [HttpPost("firm")]
    public async Task<IActionResult> AddClientFirmToBase(AddClientFirmDTO clientFirmDto)
    {
        await clientService.AddClientFirmToBase(clientFirmDto);
        return Ok("Firm was added");
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("physical/{id:int}/update")]
    public async Task<IActionResult> UpdateClientPhysicalToBase(int id, UpdateClientPhysicalDTO clientPhysicalDto)
    {
        await clientService.UpdateClientPhysicalInBase(id, clientPhysicalDto);
        return Ok("Physical client was updated");
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("firm/{id:int}/update")]
    public async Task<IActionResult> UpdateClientFirmToBase(int id, UpdateClientFirmDTO clientFirmDto)
    {
        await clientService.UpdateClientFirmInBase(id ,clientFirmDto);
        return Ok("Firm was updated");
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("physical/{id:int}")]
    public async Task<IActionResult> DeleteClientPhysicalFromBase(int id)
    {
        await clientService.DeleteClientPhysicalFromBase(id);
        return Ok("Physical client was deleted");
    }
}
