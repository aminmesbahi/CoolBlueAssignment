using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public interface IProductService
{
    public Task<InsuranceDto> GetProductInsuranceInfo(int productId);
    public Task<bool> IsProductTypeExistsAsync(int productTypeId);
    public Task<bool> IsProductExistsAsync(int productId);
}