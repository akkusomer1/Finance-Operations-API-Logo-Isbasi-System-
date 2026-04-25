using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface ICustomerCategoryRepository
{
    IQueryable<CustomerCategory> CustomerCategories { get; }
    Task<List<CustomerCategory>> GetListAsync();
    Task<CustomerCategory> GetByIdAsync(int customerCategoryId);
    Task<int> InsertAsync(CustomerCategory customerCategory);
    Task UpdateAsync(CustomerCategory customerCategory);
    Task DeleteAsync(CustomerCategory customerCategory);
}
