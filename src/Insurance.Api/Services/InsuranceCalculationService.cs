using Insurance.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public class InsuranceCalculationService : IInsuranceCalculationService
{
    private readonly IProductService _productService;
    private readonly ISurchargeService _surchargeService;
    public InsuranceCalculationService(IProductService productService, ISurchargeService surchargeService)
    {
        _productService = productService;
        _surchargeService = surchargeService;
    }

    public async Task<OrderResponseDto> CalculateOrderInsuranceAsync(OrderRequestDto toInsure)
    {
        OrderResponseDto response = new();
        response.Items = new List<OrderResponseItem>();
        bool digitalCameraIncluded = false;
        foreach (var item in toInsure.Items)
        {
            var productInsurance = await CalculateProductInsuranceAsync(item.ProductId);
            var orderResponseItem = new OrderResponseItem(productInsurance.ProductId, item.Quantity, productInsurance.ProductTypeId, productInsurance.ProductTypeName, productInsurance.InsuranceValue);

            if (orderResponseItem.ProductTypeName.ToLower() == "digital cameras" && item.Quantity >= 1)
            {
                digitalCameraIncluded = true;
            }
            var surcharges = _surchargeService.GetProductTypeSurcharges(orderResponseItem.ProductTypeId);
            if (surcharges != null && surcharges.SurchargeRates != null)
            {
                orderResponseItem.Surcharges = new List<SurchargeItem>();
                foreach (var surcharge in surcharges.SurchargeRates)
                {
                    orderResponseItem.Surcharges.Add(new SurchargeItem(surcharge.Title, surcharge.Rate));
                }
            }
            response.Items.Add(orderResponseItem);
        }
        if (digitalCameraIncluded)
        {
            response.AdditionalInsuranceCost.Add(new AdditionalInsuranceCostItem("Digital Camera Insurance", 500));
        }

        return response;
    }

    public async Task<InsuranceDto> CalculateProductInsuranceAsync(int productId)
    {
        var toInsure = await _productService.GetProductInsuranceInfo(productId);
        if (toInsure == default)
            return default;
        var surcharges = _surchargeService.GetProductTypeSurcharges(toInsure.ProductTypeId);
        if (toInsure.CanBeInsured)
        {
            if (toInsure.SalesPrice >= 500 && toInsure.SalesPrice < 2000)
                toInsure.InsuranceValue += 1000;

            if (toInsure.SalesPrice >= 2000)
                toInsure.InsuranceValue += 2000;

            if (toInsure.ProductTypeName.ToLower() == "laptops" || toInsure.ProductTypeName.ToLower() == "smartphones")
                toInsure.InsuranceValue += 500;
        }
        if (surcharges != default && surcharges.SurchargeRates != null)
        {
            toInsure.SurchargeRates = new List<SurchargeItem>();
            foreach (var item in surcharges.SurchargeRates)
            {
                toInsure.SurchargeRates.Add(new SurchargeItem(item.Title, item.Rate));
            }
        }
        return toInsure;
    }
}
