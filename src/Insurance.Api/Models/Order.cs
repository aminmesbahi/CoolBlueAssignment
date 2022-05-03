using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Models;

public record Order
{
    public int Id { get; set; }
    public IEnumerable<OrderItem> Items { get; set; }
    public Dictionary<string, decimal> AdditionalInsuranceCost { get; set; } = new Dictionary<string, decimal>();
    public decimal TotalInsuranceCost { get => Items.Sum(i => i.TotalInsuranceCost) + AdditionalInsuranceCost.Sum(i => i.Value); }
}

public record OrderItem
{
    public InsuranceDto Item { get; set; }
    public int Quantity { get; set; }
    public decimal TotalInsuranceCost { get => Quantity * Item.InsuranceValue; }
    public decimal TotalPrice { get => Quantity * Item.SalesPrice; }
    public decimal TotalSurchargeRates { get => Item.SurchargeRates.Sum(x => x.Value); }
}

