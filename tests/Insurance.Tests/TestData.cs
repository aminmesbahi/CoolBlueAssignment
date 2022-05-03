using Insurance.Api.Models;
using System.Collections.Generic;

namespace Insurance.Tests;

public static class TestData
{
    public static void Init()
    {
        ProductTypes = new List<ProductType>();
        ProductTypes.Add(new ProductType(1, "Laptops", true));
        ProductTypes.Add(new ProductType(2, "Smartphones", true));
        ProductTypes.Add(new ProductType(3, "Digital cameras", true));
        ProductTypes.Add(new ProductType(4, "SLR cameras", false));
        ProductTypes.Add(new ProductType(5, "MP3 players", false));
        ProductTypes.Add(new ProductType(6, "Washing machines", true));

        Products = new List<Product>();
        Products.Add(new Product(1, "Test Laptop #1, 100€", 1, 100));
        Products.Add(new Product(2, "Test Laptop #2, 500€", 1, 500));
        Products.Add(new Product(3, "Test Laptop #3, 2000€", 1, 2000));

        Products.Add(new Product(4, "Test Smartphone #1, 100€", 2, 100));
        Products.Add(new Product(5, "Test Smartphone #2, 500€", 2, 500));
        Products.Add(new Product(6, "Test Smartphone #3, 2000€", 2, 2000));

        Products.Add(new Product(7, "Test general product #1, 100€", 6, 100));
        Products.Add(new Product(8, "Test general product #2, 500€", 6, 500));
        Products.Add(new Product(9, "Test general product #3, 2000€", 6, 2000));

        Products.Add(new Product(10, "Test not insurable product #1, 100€", 5, 100));
        Products.Add(new Product(11, "Test not insurable product #2, 500€", 5, 500));
        Products.Add(new Product(12, "Test not insurable product #3, 2000€", 5, 2000));

        Products.Add(new Product(13, "Test Digital Camera #1, 100€", 3, 100));
        Products.Add(new Product(14, "Test Digital Camera #2, 500€", 3, 500));
        Products.Add(new Product(15, "Test Digital Camera #3, 2000€", 3, 2000));
    }
    public static List<ProductType> ProductTypes { get; set; }
    public static List<Product> Products { get; set; }
}


