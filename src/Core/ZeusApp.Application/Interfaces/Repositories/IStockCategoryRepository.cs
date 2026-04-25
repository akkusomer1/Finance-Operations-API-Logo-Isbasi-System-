using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IStockCategoryRepository
{
    IQueryable<StockCategory> StockCategories { get; }
    Task<List<StockCategory>> GetListAsync();
    Task<StockCategory> GetByIdAsync(int stockCategoryId);
    Task<int> InsertAsync(StockCategory stockCategory);
    Task UpdateAsync(StockCategory stockCategory);
    Task DeleteAsync(StockCategory stockCategory);
}