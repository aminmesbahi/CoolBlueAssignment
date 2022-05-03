namespace Insurance.Api.Models;
public class Product
{
    public Product(int _id, string _name, int _productTypeId, decimal _salesPrice)
    {
        Id = _id;
        Name = _name;
        ProductTypeId = _productTypeId;
        SalesPrice = _salesPrice;
    }
    public Product()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public decimal SalesPrice { get; set; }
}
