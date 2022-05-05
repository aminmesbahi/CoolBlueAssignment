namespace Insurance.Api.Models;
public class Product
{
    public Product(int id, string name, int productTypeId, decimal salesPrice)
    {
        Id = id;
        Name = name;
        ProductTypeId = productTypeId;
        SalesPrice = salesPrice;
    }
    public Product()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public decimal SalesPrice { get; set; }
}
