using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IProductServiceRepository
{
    IQueryable<ProductService> ProductServices { get; }
    Task<List<ProductService>> GetListAsync();
    Task<ProductService> GetByIdAsync(int productServiceId);
    Task<int> InsertAsync(ProductService productService);
    Task UpdateAsync(ProductService productService);
    Task DeleteAsync(ProductService productService);
}
