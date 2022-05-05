using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Insurance.Api.Models;

public class InsuranceDto
{
    public InsuranceDto(int productId, int productTypeId, string productTypeName, bool canBeInsured, decimal salesPrice)
    {
        ProductId = productId;
        ProductTypeId = productTypeId;
        ProductTypeName = productTypeName;
        CanBeInsured = canBeInsured;
        SalesPrice = salesPrice;
    }
    public InsuranceDto()
    {

    }
    public int ProductId { get; set; }
    public decimal InsuranceValue { get; set; }
    public decimal TotalSurcharge { get => SurchargeRates.Sum(x => x.Rate); }
    [JsonIgnore]
    public int ProductTypeId { get; set; }
    [JsonIgnore]
    public string ProductTypeName { get; set; }
    [JsonIgnore]
    public bool CanBeInsured { get; set; }
    [JsonIgnore]
    public decimal SalesPrice { get; set; }
    public ICollection<SurchargeItem> SurchargeRates { get; set; } = new List<SurchargeItem>();
}
