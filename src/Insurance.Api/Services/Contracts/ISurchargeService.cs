using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public interface ISurchargeService
{
    public Task AddSurchargeAsync(Surcharge surcharge);
    public SurchargeDto GetProductTypeSurcharges(int productTypeId);
}
