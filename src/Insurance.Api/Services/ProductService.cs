using Insurance.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public class ProductService : IProductService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProductService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(_configuration.GetSection("ProductApi").Value);

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, "CoolBlueInsurance");
    }

    public async Task<InsuranceDto> GetProductInsuranceInfo(int productID)
    {
        var productTypes = await _httpClient.GetFromJsonAsync<IEnumerable<ProductType>>(
            "product_types");

        var product = await _httpClient.GetFromJsonAsync<Product>(string.Format("/products/{0:G}", productID));
        var productType = productTypes.Where(pt => pt.Id == product.ProductTypeId).FirstOrDefault();
        var insurance = new InsuranceDto();
        if (productType != null && product != null)
        {
            insurance.ProductTypeName = productType.Name;
            insurance.ProductTypeHasInsurance = productType.CanBeInsured;
            insurance.ProductId = productID;
            insurance.InsuranceValue = 0;
            insurance.SalesPrice = product.SalesPrice;
        }
        else
        {
            throw new Exception(message: "The given product with proper product type didn't found!\nProduct id: {productID}, Product type id: {product.ProductTypeId}");
        }
        return insurance;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}