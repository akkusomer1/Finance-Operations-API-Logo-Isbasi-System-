using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class StockCategoryRepository : IStockCategoryRepository
{
    private readonly IRepositoryAsync<StockCategory> _repository;

    public StockCategoryRepository(IRepositoryAsync<StockCategory> repository)
    {
        _repository = repository;
    }

    public IQueryable<StockCategory> StockCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(StockCategory stockCategory)
    {
        stockCategory.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(stockCategory);
        //await _repository.DeleteAsync(stockCategory);
    }

    public async Task<StockCategory> GetByIdAsync(int stockCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == stockCategoryId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<StockCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(StockCategory stockCategory)
    {
        await _repository.AddAsync(stockCategory);
        return stockCategory.Id;
    }

    public async Task UpdateAsync(StockCategory stockCategory)
    {
        await _repository.UpdateAsync(stockCategory);
    }
}
