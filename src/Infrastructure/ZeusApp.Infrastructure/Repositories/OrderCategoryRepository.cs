using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.Infrastructure.Repositories;
public class OrderCategoryRepository:IOrderCategoryRepository
{
    private readonly IRepositoryAsync<OrderCategory> _repository;
    public OrderCategoryRepository(IRepositoryAsync<OrderCategory> repository)
    {
        _repository = repository;
    }
    public IQueryable<OrderCategory> OrderCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);


    public async Task DeleteAsync(OrderCategory orderCategory)
    {
        await _repository.DeleteAsync(orderCategory);
    }

    public async Task<OrderCategory> GetByIdAsync(int orderCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == orderCategoryId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<OrderCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(OrderCategory orderCategory)
    {
        await _repository.AddAsync(orderCategory);
        return orderCategory.Id;
    }

    public async Task UpdateAsync(OrderCategory orderCategory)
    {
        await _repository.UpdateAsync(orderCategory);
    }
}
