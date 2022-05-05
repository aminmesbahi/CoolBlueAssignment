using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SurchargeController : ControllerBase
{
    private readonly ISurchargeService _surchargeService;
    public SurchargeController(ISurchargeService surchargeService)
    {
        _surchargeService = surchargeService;
    }

    /// <summary>
    /// Returns all surcharges for the given product type.
    /// </summary>
    /// <param name="id">Product Type ID</param>
    /// <returns>Returns InsuranceValue, TotalSurcharge, SurchargeRates</returns>
    /// <remarks>
    /// Sample Product Type IDs:
    ///
    ///     21: Laptops
    ///     32: Smartphones
    ///     33: Digital cameras
    ///
    /// </remarks>
    /// <response code="200">Returns surcharges when product type exists</response>
    /// <response code="404">If the given product type id doesn't exists</response>    
    [HttpGet]
    [Route("surcharges/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SurchargeDto> Get(int id)
    {
        var result = _surchargeService.GetProductTypeSurcharges(id);
        return result is null ? NotFound() : result;
    }

    /// <summary>
    /// Adds surcharge to a specific product type.
    /// </summary>
    /// <param name="surcharge"></param>
    /// <returns>A newly created Surcharge for the given Product Type</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /surcharges
    ///     {
    ///        "productTypeId": 21,
    ///        "title": "Extra shipping cost for Laptops #1",
    ///        "surchargeRate": 400
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created surcharge</response>
    /// <response code="400">If the item is null or not in correct format</response>
    [HttpPost]
    [Route("surcharges")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SurchargeItem>> PostSurcharge(Surcharge surcharge)
    {
        try
        {
            await _surchargeService.AddSurchargeAsync(surcharge);
            return StatusCode(StatusCodes.Status201Created, surcharge);
        }
        catch
        {
            return BadRequest();
        }

    }
}
