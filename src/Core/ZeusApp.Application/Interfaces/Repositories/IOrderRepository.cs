using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IOrderRepository
{
    IQueryable<Order> Orders { get; }
    Task<List<Order>> GetListAsync();
    Task<Order> GetByIdAsync(int orderId);
    Task<int> InsertAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}
