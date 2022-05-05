using System.Text.Json.Serialization;

namespace Insurance.Api.Models;

public class Surcharge
{
    public Surcharge(int id, int productTypeId, string title, decimal surchargeRate)
    {
        Id = id;
        ProductTypeId = productTypeId;
        Title = title;
        SurchargeRate = surchargeRate;
    }
    public Surcharge()
    {

    }

    [JsonIgnore]
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string Title { get; set; }
    public decimal SurchargeRate { get; set; }
}
