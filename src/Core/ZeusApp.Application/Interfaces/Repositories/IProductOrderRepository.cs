using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IProductOrderRepository
{
    IQueryable<ProductOrder> ProductOrders { get; }
    Task<List<ProductOrder>> GetListAsync();
    Task<ProductOrder> GetByIdAsync(int productOrderId);
    Task<int> InsertAsync(ProductOrder productOrderId);
    Task UpdateAsync(ProductOrder productOrderId);
    Task DeleteAsync(ProductOrder productOrderId);
}
