using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IStockRepository
{
    IQueryable<Stock> Stocks { get; }
    Task<List<Stock>> GetListAsync();
    Task<Stock> GetByIdAsync(int stockId);
    Task<int> InsertAsync(Stock stock);
    Task UpdateAsync(Stock stock);
    Task DeleteAsync(Stock stock);
}
