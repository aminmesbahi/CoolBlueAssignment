using Insurance.Api.Data;
using Insurance.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    public class SurchargeService : ISurchargeService
    {
        private readonly InsuranceDbContext _context;
        private readonly IProductService _productService;
        public SurchargeService(IServiceProvider serviceProvider, IProductService productService)
        {
            _context = serviceProvider.GetRequiredService<InsuranceDbContext>();
            _productService = productService;
        }
        public Task AddSurchargeAsync(Surcharge surcharge)
        {
            if (_productService.IsProductTypeExistsAsync(surcharge.ProductTypeId).Result)
            {
                _context.Surcharges.Add(surcharge);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else { throw new Exception(message: $"Product type with the given Id: {surcharge.ProductTypeId} doesn't exeists."); }
        }

        public SurchargeDto GetProductTypeSurcharges(int productTypeId)
        {
            if (_productService.IsProductTypeExistsAsync(productTypeId).Result)
            {
                return new SurchargeDto(productTypeId,
                    _context.Surcharges.Where(e => e.ProductTypeId == productTypeId).OrderBy(e => e.Id).Select(e => new SurchargeItem(e.Title, e.SurchargeRate)).ToList());
            }
            else return default;
        }
    }
}
