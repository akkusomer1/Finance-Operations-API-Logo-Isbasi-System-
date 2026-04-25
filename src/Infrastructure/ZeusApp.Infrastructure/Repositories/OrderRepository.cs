using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class OrderRepository:IOrderRepository
{
    private readonly IRepositoryAsync<Order> _repository;

    public OrderRepository(IRepositoryAsync<Order> repository)
    {
        _repository = repository;
    }

    public IQueryable<Order> Orders => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Order order)
    {
        order.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(order);
        //await _repository.DeleteAsync(Order);
    }

    public async Task<Order> GetByIdAsync(int orderId)
    {
        return await _repository.Entities
            .Include(x=>x.ProductOrders)            
            .Include(x=>x.CustomerSupplier)
            .Where(p => p.Id == orderId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Order>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Order order)
    {
        await _repository.AddAsync(order);
        return order.Id;
    }

    public async Task UpdateAsync(Order order)
    {
        await _repository.UpdateAsync(order);
    }
}
