using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Contrib.HttpClient;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Tests;

public class InsuranceCalculationTests : IClassFixture<ProductServiceFixture>
{
    IInsuranceCalculationService _insuranceService;
    readonly ProductServiceFixture _fixture;
    public InsuranceCalculationTests(ProductServiceFixture fixture)
    {
        _fixture = fixture;
        _insuranceService = new InsuranceCalculationService(new ProductService(_fixture.Client.CreateClient(), _fixture.Config.Object));
    }

    #region Tests for Order Insurance Calculation
    [Fact]
    [Trait("Order", "Simple")]
    public async Task CalculateOrderInsurance_GivenOrderWithMultipleItemsFromSameProductType_ShouldReturn2000EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 2000;

        var dto = new Order
        {
            Id = 1,
            Items = new[] { new OrderItem { Item = new InsuranceDto { ProductId = 1 }, Quantity = 1 }, new OrderItem { Item = new InsuranceDto { ProductId = 2 }, Quantity = 1 } }
        };
        var result = await _insuranceService.CalculateOrderInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.TotalInsuranceCost
        );
    }
    [Fact]
    [Trait("Order", "Simple")]
    public async Task CalculateOrderInsurance_GivenOrderWithMultipleItemsFromDifferentProductType_ShouldReturn500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 500;

        var dto = new Order
        {
            Id = 1,
            Items = new[] { new OrderItem { Item = new InsuranceDto { ProductId = 1 }, Quantity = 1 }, new OrderItem { Item = new InsuranceDto { ProductId = 7 }, Quantity = 1 } }
        };
        var result = await _insuranceService.CalculateOrderInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.TotalInsuranceCost
        );
    }

    [Fact]
    [Trait("Order", "Simple")]
    public async Task CalculateOrderInsurance_GivenOrderWithMultipleItemsAndQuantityGreaterThan1FromDifferentProductType_ShouldReturn1000EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 1000;

        var dto = new Order
        {
            Id = 1,
            Items = new[] { new OrderItem { Item = new InsuranceDto { ProductId = 1 }, Quantity = 2 }, new OrderItem { Item = new InsuranceDto { ProductId = 7 }, Quantity = 2 } }
        };
        var result = await _insuranceService.CalculateOrderInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.TotalInsuranceCost
        );
    }
    [Fact]
    [Trait("Order", "DigitalCameraIncluded")]
    public async Task CalculateOrderInsurance_GivenOrderWithMultipleItemsAndQuantityGreaterThan1FromDifferentProductTypeAndOrderAdditionalInsurance_ShouldReturn2500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 2500;

        var dto = new Order
        {
            Id = 1,
            Items = new[] { new OrderItem { Item = new InsuranceDto { ProductId = 1 }, Quantity = 2 }, new OrderItem { Item = new InsuranceDto { ProductId = 14 }, Quantity = 1 } }
        };
        var result = await _insuranceService.CalculateOrderInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.TotalInsuranceCost
        );
    }
    [Fact]
    [Trait("Order", "DigitalCameraIncluded")]
    public async Task CalculateOrderInsurance_GivenOrderWithMultipleItemsAndDifferentProductTypeAndOrderAdditionalInsurance_ShouldReturn2000EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 1500;

        var dto = new Order
        {
            Id = 1,
            Items = new[] { new OrderItem { Item = new InsuranceDto { ProductId = 13 }, Quantity = 1 }, new OrderItem { Item = new InsuranceDto { ProductId = 14 }, Quantity = 1 } }
        };
        var result = await _insuranceService.CalculateOrderInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.TotalInsuranceCost
        );
    }
    #endregion Tests for Order Insurance Calculation

    #region Tests for laptops and smartphones
    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenLaptopSalesPriceLessThan500Euros_ShouldAdd500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 500;

        var dto = new InsuranceDto
        {
            ProductId = 1,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenLaptopSalesPriceBetween500And2000Euros_ShouldAdd1500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 1500;

        var dto = new InsuranceDto
        {
            ProductId = 2,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenLaptopSalesPriceGreaterThan2000Euros_ShouldAdd2500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 2500;

        var dto = new InsuranceDto
        {
            ProductId = 3,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenSmartphoneSalesPriceLessThan500Euros_ShouldAdd500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 500;

        var dto = new InsuranceDto
        {
            ProductId = 4,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenSmartphoneSalesPriceBetween500And2000Euros_ShouldAdd1500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 1500;

        var dto = new InsuranceDto
        {
            ProductId = 5,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Laptop&SmartPhone")]
    public async Task CalculateProductInsurance_GivenSmartphoneSalesPriceGreaterThan2000Euros_ShouldAdd2500EurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 2500;

        var dto = new InsuranceDto
        {
            ProductId = 6,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }
    #endregion Tests for laptops and smartphones

    #region Tests for common products
    [Fact]
    [Trait("Product", "Common")]
    public async Task CalculateProductInsurance_GivenSalesPriceLessThan500Euros_ShouldAddZeroEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 0;

        var dto = new InsuranceDto
        {
            ProductId = 7,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Common")]
    public async Task CalculateProductInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 1000;

        var dto = new InsuranceDto
        {
            ProductId = 8,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "Common")]
    public async Task CalculateProductInsurance_GivenSalesPriceGreaterThan2000Euros_ShouldAddTwoThousandEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 2000;

        var dto = new InsuranceDto
        {
            ProductId = 9,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }
    #endregion Tests for common products

    #region Tests for not insurable products
    [Fact]
    [Trait("Product", "NoInsurance")]
    public async Task CalculateProductInsurance_GivenSalesPriceLessThan500EurosNotInsurable_ShouldAddZeroEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 0;
        var dto = new InsuranceDto
        {
            ProductId = 10,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "NoInsurance")]
    public async Task CalculateProductInsurance_GivenSalesPriceBetween500And2000EurosNotInsurable_ShouldAddZeroEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 0;
        var dto = new InsuranceDto
        {
            ProductId = 11,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }

    [Fact]
    [Trait("Product", "NoInsurance")]
    public async Task CalculateProductInsurance_GivenSalesPriceGreaterThan2000EurosNotInsurable_ShouldAddZeroEurosToInsuranceCost()
    {
        const decimal expectedInsuranceValue = 0;
        var dto = new InsuranceDto
        {
            ProductId = 12,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expectedInsuranceValue,
            actual: result.InsuranceValue
        );
    }
    #endregion Tests for not insurable products

    #region Example aggregated tests for laptop and smartphone products
    [Theory]
    [Trait("Product", "Theory, Laptop&SmartPhone")]
    [InlineData(1, 500)]
    [InlineData(2, 1500)]
    [InlineData(3, 2500)]
    [InlineData(4, 500)]
    [InlineData(5, 1500)]
    [InlineData(6, 2500)]
    public async Task CalculateProductInsurance_GivenProductsSalesPriceLessThan500Euros_ShouldAdd500EurosBasedOnTheirProductTypesToInsuranceCost(int productId, decimal expected)
    {
        var dto = new InsuranceDto
        {
            ProductId = productId,
        };
        var result = await _insuranceService.CalculateProductInsuranceAsync(dto);

        Assert.Equal(
            expected: expected,
            actual: result.InsuranceValue
        );
    }
    #endregion Example aggregated tests for laptop and smartphone products
}

public class ProductServiceFixture
{
    public ProductServiceFixture()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("testsettings.json", false, true)
            .Build();
        ProductServiceBaseUrl = config.GetSection("ProductApi").Value;

        Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x.Value).Returns(ProductServiceBaseUrl);
        Config = new Mock<IConfiguration>();
        Config.Setup(x => x.GetSection(It.Is<string>(k => k == "ProductApi"))).Returns(mockSection.Object);

        TestData.Init();
        Handler = new Mock<HttpMessageHandler>();
        Client = Handler.CreateClientFactory();
        Handler.SetupRequest(HttpMethod.Get, $"{ProductServiceBaseUrl}/product_types")
            .ReturnsResponse(System.Text.Json.JsonSerializer.Serialize(TestData.ProductTypes), "application/json");
        foreach (var product in TestData.Products)
        {
            Handler.SetupRequest(HttpMethod.Get, $"{ProductServiceBaseUrl}/products/{product.Id}")
              .ReturnsResponse(System.Text.Json.JsonSerializer.Serialize(product), "application/json");
        }
    }

    public IHttpClientFactory Client { get; private set; }
    public Mock<HttpMessageHandler> Handler { get; set; }
    public Mock<IConfiguration> Config { get; private set; }
    public string ProductServiceBaseUrl { get; set; }
}