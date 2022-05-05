using System.Collections.Generic;

namespace Insurance.Api.Models;

public class ProductType
{
    public ProductType(int id, string name, bool canBeInsured)
    {
        Id = id;
        Name = name;
        CanBeInsured = canBeInsured;
    }
    public ProductType()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public bool CanBeInsured { get; set; }
    public ICollection<SurchargeItem> SurchargeItems { get; set; }
}