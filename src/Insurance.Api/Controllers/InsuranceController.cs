using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;

public class InsuranceController : ControllerBase
{
    private readonly IInsuranceCalculationService _insuranceCalculationService;
    public InsuranceController(IInsuranceCalculationService insuranceCalculationService)
    {
        _insuranceCalculationService = insuranceCalculationService;
    }
    [HttpPost]
    [Route("api/insurance/product")]
    public async Task<InsuranceDto> CalculateProductInsuranceAsync([FromBody] InsuranceDto toCalculateInsure)
        => await _insuranceCalculationService.CalculateProductInsuranceAsync(toCalculateInsure);

    [HttpPost]
    [Route("api/insurance/order")]
    public async Task<Order> CalculateOrderInsuranceAsync([FromBody] Order toCalculateInsure)
        => await _insuranceCalculationService.CalculateOrderInsuranceAsync(toCalculateInsure);
}


