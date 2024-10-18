using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Service;

namespace Project.Controller;

[ApiController]
[Route("contracts")]
public class ContractController(IContractService contractService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateContract(ContractCreateDTO contractCreate)
    {
        await contractService.CreateContract(contractCreate);
        return Ok("Contract was created");
    }
    
    [Authorize]
    [HttpPatch("{idContract:int}")]
    public async Task<IActionResult> MakePaymentForContract(int idContract, PaymentDTO payment)
    {
        await contractService.MakePaymentForContract(idContract, payment);
        return Ok("Payment was made");
    }
}