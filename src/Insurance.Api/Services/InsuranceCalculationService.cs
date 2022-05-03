using Insurance.Api.Models;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public class InsuranceCalculationService : IInsuranceCalculationService
{
    private readonly IProductService _productService;
    public InsuranceCalculationService(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Order> CalculateOrderInsuranceAsync(Order toInsure)
    {
        bool digitalCameraIncluded = false;
        foreach (var item in toInsure.Items)
        {
            item.Item = await CalculateProductInsuranceAsync(item.Item);
            if (item.Item.ProductTypeName.ToLower() == "digital cameras" && item.Quantity >= 1)
            {
                digitalCameraIncluded = true;
            }
        }
        if (digitalCameraIncluded)
        {
            toInsure.AdditionalInsuranceCost.Add("Digital Camera Insurance", 500);
        }
        return toInsure;
    }

    public async Task<InsuranceDto> CalculateProductInsuranceAsync(InsuranceDto toInsure)
    {
        toInsure = await _productService.GetProductInsuranceInfo(toInsure.ProductId);
        if (toInsure.ProductTypeHasInsurance)
        {
            if (toInsure.SalesPrice >= 500 && toInsure.SalesPrice < 2000)
                toInsure.InsuranceValue += 1000;

            if (toInsure.SalesPrice >= 2000)
                toInsure.InsuranceValue += 2000;

            if (toInsure.ProductTypeName.ToLower() == "laptops" || toInsure.ProductTypeName.ToLower() == "smartphones")
                toInsure.InsuranceValue += 500;
        }
        return toInsure;
    }
}
