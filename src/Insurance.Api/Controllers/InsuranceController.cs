using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class InsuranceController : ControllerBase
{
    private readonly IInsuranceCalculationService _insuranceCalculationService;
    public InsuranceController(IInsuranceCalculationService insuranceCalculationService)
    {
        _insuranceCalculationService = insuranceCalculationService;
    }

    /// <summary>
    /// Calculates the insurance rate for the given product.
    /// </summary>
    /// <param name="productId">The Product Id you want to know its insurance cost</param>
    /// <returns>Insurance details for the given Product</returns>
    /// <remarks>
    /// Sample Product IDs:
    ///
    ///     861866: Apple MacBook Pro 13 (2020) MXK52N/A Space Gray, 1749 Euros
    ///     837856: Lenovo Chromebook C330-11 81HY000MMH, 299 Euros
    ///     819148: Nikon D3500 + AF-P DX 18-55mm f/3.5-5.6G VR, 469 Euros
    ///
    /// </remarks>
    /// <response code="200">Returns surcharges when product type exists</response>
    /// <response code="404">If the given product type id doesn't exists</response>
    [HttpGet]
    [Route("products/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsuranceDto>> CalculateProductInsuranceAsync(int productId)
    {
        var result = await _insuranceCalculationService.CalculateProductInsuranceAsync(productId);
        if (result == null)
            return NotFound();
        else
            return Ok(result);
    }


    /// <summary>
    /// Calculates the total insurance cost for the given order according to included products.
    /// </summary>
    /// <param name="toCalculateInsure"></param>
    /// <returns>Insurance details for the given Order</returns>
    /// <remarks>
    /// Sample Order:
    ///
    ///    {
    ///       "items": [
    ///         {
    ///           "productId": 861866,
    ///           "quantity": 1
    ///         },
    ///         {
    ///           "productId": 780829,
    ///           "quantity": 1
    ///         },
    ///         {
    ///         "productId": 836194,
    ///           "quantity": 3
    ///         }
    ///       ]
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns Insurance and Surcharge calculations for the given order</response>
    [HttpPost]
    [Route("order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<OrderResponseDto> CalculateOrderInsuranceAsync([FromBody] OrderRequestDto toCalculateInsure)
        => await _insuranceCalculationService.CalculateOrderInsuranceAsync(toCalculateInsure);
}


