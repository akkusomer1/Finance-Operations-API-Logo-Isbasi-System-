using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.Infrastructure.Repositories;
public class ProductServiceRepository : IProductServiceRepository
{
    private readonly IRepositoryAsync<ProductService> _repository;

    public ProductServiceRepository(IRepositoryAsync<ProductService> repository)
    {
        _repository = repository;
    }

    public IQueryable<ProductService> ProductServices => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(ProductService productService)
    {
        productService.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(productService);
    }

    public async Task<ProductService> GetByIdAsync(int productServiceId)
    {
        return _repository
             .Entities
             .Include(x => x.ProductServiceCategory)
            .Include(x => x.ProductBrand)
            .Include(x => x.Unit)
            .Include(x => x.ServiceGroup)
            .Where(p => p.Id == productServiceId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync()!.Result!;
    }

    public async Task<List<ProductService>> GetListAsync()
    {
        return await _repository.Entities
            .Include(x => x.Unit)
            .Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ProductService productService)
    {
        await _repository.AddAsync(productService);
        return productService.Id;
    }

    public async Task UpdateAsync(ProductService productService)
    {
        await _repository.UpdateAsync(productService);
    }
}
