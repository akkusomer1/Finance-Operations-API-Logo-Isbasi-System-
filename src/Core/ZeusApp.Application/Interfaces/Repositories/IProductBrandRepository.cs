using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IProductBrandRepository
{
    IQueryable<ProductBrand> ProductBrands { get; }
    Task<List<ProductBrand>> GetListAsync();
    Task<ProductBrand> GetByIdAsync(int productBrandId);
    Task<int> InsertAsync(ProductBrand productBrand);
    Task UpdateAsync(ProductBrand productBrand);
    Task DeleteAsync(ProductBrand productBrand);
}
