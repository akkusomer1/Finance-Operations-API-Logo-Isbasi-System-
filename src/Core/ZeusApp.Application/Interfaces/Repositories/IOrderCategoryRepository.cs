using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IOrderCategoryRepository
{
    IQueryable<OrderCategory> OrderCategories { get; }
    Task<List<OrderCategory>> GetListAsync();
    Task<OrderCategory> GetByIdAsync(int orderCategoryId);
    Task<int> InsertAsync(OrderCategory orderCategory);
    Task UpdateAsync(OrderCategory orderCategory);
    Task DeleteAsync(OrderCategory orderCategory);
}
