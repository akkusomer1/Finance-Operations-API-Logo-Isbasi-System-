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
public class ProductServiceCategoryRepository : IProductServiceCategoryRepository
{
    private readonly IRepositoryAsync<ProductServiceCategory> _repository;

    public ProductServiceCategoryRepository(IRepositoryAsync<ProductServiceCategory> repository)
    {
        _repository = repository;
    }

    public IQueryable<ProductServiceCategory> ProductServiceCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(ProductServiceCategory productServiceCategoryId)
    {
        productServiceCategoryId.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(productServiceCategoryId);
    }

    public async Task<ProductServiceCategory> GetByIdAsync(int productServiceCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == productServiceCategoryId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ProductServiceCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ProductServiceCategory productServiceCategory)
    {
        await _repository.AddAsync(productServiceCategory);
        return productServiceCategory.Id;
    }

    public async Task UpdateAsync(ProductServiceCategory productServiceCategory)
    {
        await _repository.UpdateAsync(productServiceCategory);
    }
}


