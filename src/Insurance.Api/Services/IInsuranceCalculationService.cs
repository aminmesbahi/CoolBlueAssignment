using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public interface IInsuranceCalculationService
{
    public Task<InsuranceDto> CalculateProductInsuranceAsync(InsuranceDto toInsure);
    public Task<Order> CalculateOrderInsuranceAsync(Order toInsure);
}
