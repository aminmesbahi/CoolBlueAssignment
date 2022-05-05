using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public interface IInsuranceCalculationService
{
    public Task<InsuranceDto> CalculateProductInsuranceAsync(int productId);
    public Task<OrderResponseDto> CalculateOrderInsuranceAsync(OrderRequestDto toInsure);
}
