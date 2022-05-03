using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public interface IProductService
{
    public Task<InsuranceDto> GetProductInsuranceInfo(int productID);
}