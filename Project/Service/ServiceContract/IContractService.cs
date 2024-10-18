using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Service;

public interface IContractService
{
    public Task CreateContract(ContractCreateDTO contractCreate);
    public Task MakePaymentForContract(int id, PaymentDTO payment);
}