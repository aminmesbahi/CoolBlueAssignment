using System.Collections.Generic;

namespace Insurance.Api.Models;

public class SurchargeDto
{
    public SurchargeDto(int productTypeId, ICollection<SurchargeItem> surchargeRates)
    {
        ProductTypeId = productTypeId;
        SurchargeRates = surchargeRates;
    }

    public int ProductTypeId { get; set; }
    public ICollection<SurchargeItem> SurchargeRates { get; set; }
}
public class SurchargeItem
{
    public SurchargeItem(string title, decimal rate)
    {
        Title = title;
        Rate = rate;
    }

    public string Title { get; set; }
    public decimal Rate { get; set; }
}