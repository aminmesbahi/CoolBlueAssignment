using System.Collections.Generic;

namespace Insurance.Api.Models;

public class ProductType
{
    public ProductType(int _id, string _name, bool _canBeInsured)
    {
        Id = _id;
        Name = _name;
        CanBeInsured = _canBeInsured;
    }
    public ProductType()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public bool CanBeInsured { get; set; }
    public Dictionary<string, decimal> SurchargeRates { get; set; }
}