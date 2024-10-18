using Project.DTO;

namespace Project.Service;

public interface IRevenueService
{
    public Task<RevenueDTO> RevenueCalculation(int idObject, string? currency);
    public Task<RevenueDTO> PredictableRevenueCalculation(int idObject, string? currency);
}