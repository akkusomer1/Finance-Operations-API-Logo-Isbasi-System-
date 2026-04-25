using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IProductStockRepository
{
    IQueryable<ProductStock> ProductStocks { get; }
    Task<List<ProductStock>> GetListAsync();
    Task<ProductStock> GetByIdAsync(int productStockId);
    Task<int> InsertAsync(ProductStock productStock);
    Task UpdateAsync(ProductStock productStock);
    Task DeleteAsync(ProductStock productStock);
}
