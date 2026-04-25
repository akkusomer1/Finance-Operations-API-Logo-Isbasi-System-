using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class ProductStockRepository : IProductStockRepository
{
    private readonly IRepositoryAsync<ProductStock> _repository;

    public ProductStockRepository(IRepositoryAsync<ProductStock> repository)
    {
        _repository = repository;
    }

    public IQueryable<ProductStock> ProductStocks => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(ProductStock productStock)
    {
        productStock.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(productStock);
        //await _repository.DeleteAsync(productStock);
    }

    public async Task<ProductStock> GetByIdAsync(int productStockId)
    {
        return await _repository.Entities
            .Include(x=>x.ProductService)
            .Where(p => p.Id == productStockId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ProductStock>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ProductStock productStock)
    {
        await _repository.AddAsync(productStock);
        return productStock.Id;
    }

    public async Task UpdateAsync(ProductStock productStock)
    {
        await _repository.UpdateAsync(productStock);
    }
}
