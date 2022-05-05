using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Insurance.Api.Models;

public class OrderResponseDto
{
    public List<OrderResponseItem> Items { get; set; } = new List<OrderResponseItem>();
    public ICollection<AdditionalInsuranceCostItem> AdditionalInsuranceCost { get; set; } = new List<AdditionalInsuranceCostItem>();
    public decimal TotalInsuranceValue { get => Items.Sum(i => i.TotalInsuranceValue) + AdditionalInsuranceCost.Sum(i => i.Cost); }
    public decimal TotalSurcharge { get => Items.Sum(i => i.TotalSurcharge); }
}
public class OrderResponseItem
{
    public OrderResponseItem(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
    public OrderResponseItem(int productId, int quantity, int productTypeId, string productTypeName, decimal insuranceValue)
    {
        ProductId = productId;
        Quantity = quantity;
        ProductTypeId = productTypeId;
        ProductTypeName = productTypeName;
        InsuranceValue = insuranceValue;
    }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; }
    public decimal InsuranceValue { get; set; }
    public decimal TotalInsuranceValue { get => Quantity * InsuranceValue; }
    public decimal Surcharge { get => Surcharges.Sum(x => x.Rate); }
    public decimal TotalSurcharge { get => Surcharges.Sum(x => x.Rate) * Quantity; }
    public ICollection<SurchargeItem> Surcharges { get; set; } = new List<SurchargeItem>();
    [JsonIgnore]
    public bool ProductTypeHasInsurance { get; set; }
}

public class AdditionalInsuranceCostItem
{
    public AdditionalInsuranceCostItem(string title, decimal cost)
    {
        Title = title;
        Cost = cost;
    }

    public string Title { get; set; }
    public decimal Cost { get; set; }
}