using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;
public class StockRepository: IStockRepository
{
    private readonly IRepositoryAsync<Stock> _repository;

    public StockRepository(IRepositoryAsync<Stock> repository)
    {
        _repository = repository;
    }

    public IQueryable<Stock> Stocks => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Stock stock)
    {
         stock.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(stock);
        //await _repository.DeleteAsync(stock);
    }

    public async Task<Stock> GetByIdAsync(int stockId)
    {
        var model= await _repository.Entities
            .Include(x=>x.ProductStocks)
            .ThenInclude(x=>x.ProductService)
            .Where(p => p.Id == stockId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();

        return model!;
    }

    public async Task<List<Stock>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Stock stock)
    {
        await _repository.AddAsync(stock);
        return stock.Id;
    }

    public async Task UpdateAsync(Stock stock)
    {
        await _repository.UpdateAsync(stock);
    }
}
