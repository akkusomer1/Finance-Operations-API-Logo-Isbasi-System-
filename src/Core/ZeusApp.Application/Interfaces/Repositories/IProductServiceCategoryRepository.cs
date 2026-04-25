using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IProductServiceCategoryRepository
{
    IQueryable<ProductServiceCategory> ProductServiceCategories { get; }
    Task<List<ProductServiceCategory>> GetListAsync();
    Task<ProductServiceCategory> GetByIdAsync(int productServiceCategoryId);
    Task<int> InsertAsync(ProductServiceCategory productServiceCategoryId);
    Task UpdateAsync(ProductServiceCategory productServiceCategoryId);
    Task DeleteAsync(ProductServiceCategory productServiceCategoryId);
}
