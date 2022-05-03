using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Insurance.Api.Models;

public class InsuranceDto
{
    public int ProductId { get; set; }
    public decimal InsuranceValue { get; set; }
    [JsonIgnore]
    public string ProductTypeName { get; set; }
    [JsonIgnore]
    public bool ProductTypeHasInsurance { get; set; }
    [JsonIgnore]
    public decimal SalesPrice { get; set; }
    public Dictionary<string, decimal> SurchargeRates { get; set; }
}
