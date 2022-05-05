using Insurance.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Insurance.Api.Services;

public class ProductService : IProductService, IDisposable
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.GetSection("ProductApi").Value);

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, "CoolBlueInsurance");
    }
    public async Task<InsuranceDto> GetProductInsuranceInfo(int productId)
    {
        InsuranceDto insuranceInfo;
        Product product;
        ProductType productType = null;
        try
        {
            product = await _httpClient.GetFromJsonAsync<Product>($"/products/{productId}");
            if (product != null)
                productType = await _httpClient.GetFromJsonAsync<ProductType>($"product_types/{product.ProductTypeId}");
        }
        catch
        {
            return default;
        }
        if (productType != null)
        {
            insuranceInfo = new InsuranceDto(productId, productType.Id, productType.Name, productType.CanBeInsured, product.SalesPrice);
        }
        else
        {
            throw new Exception(message: "The given product with proper product type didn't found!\nProduct id: {productID}, Product type id: {product.ProductTypeId}");
        }
        return insuranceInfo;
    }
    public async Task<bool> IsProductTypeExistsAsync(int productTypeId)
    {
        try
        {
            var statusCode = (await _httpClient.GetAsync($"product_types/{productTypeId}")).StatusCode;
            return statusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(message: ex.Message);
        }
    }
    public async Task<bool> IsProductExistsAsync(int productId)
    {
        try
        {
            var statusCode = (await _httpClient.GetAsync($"products/{productId}")).StatusCode;
            return statusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            throw new Exception(message: ex.Message);
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}