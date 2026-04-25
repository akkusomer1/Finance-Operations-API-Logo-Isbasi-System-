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

public class ProductBrandRepository : IProductBrandRepository
{
    private readonly IRepositoryAsync<ProductBrand> _repository;

    public ProductBrandRepository(IRepositoryAsync<ProductBrand> repository)
    {
        _repository = repository;
    }

    public IQueryable<ProductBrand> ProductBrands => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(ProductBrand productBrand)
    {
        productBrand.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(productBrand);
    }

    public async Task<ProductBrand> GetByIdAsync(int productBrandId)
    {
        return await _repository.Entities.Where(p => p.Id == productBrandId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ProductBrand>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ProductBrand productBrand)
    {
        await _repository.AddAsync(productBrand);
        return productBrand.Id;
    }

    public async Task UpdateAsync(ProductBrand productBrand)
    {
        await _repository.UpdateAsync(productBrand);
    }
}

