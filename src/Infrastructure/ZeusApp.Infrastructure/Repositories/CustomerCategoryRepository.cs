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
public class CustomerCategoryRepository : ICustomerCategoryRepository
{
    private readonly IRepositoryAsync<CustomerCategory> _repository;

    public CustomerCategoryRepository(IRepositoryAsync<CustomerCategory> repository)
    {
        _repository = repository;
    }

    public IQueryable<CustomerCategory> CustomerCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(CustomerCategory customerCategory)
    {
        await _repository.DeleteAsync(customerCategory);
    }

    public async Task<CustomerCategory> GetByIdAsync(int customerCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == customerCategoryId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<CustomerCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(CustomerCategory customerCategory)
    {
        await _repository.AddAsync(customerCategory);
        return customerCategory.Id;
    }

    public async Task UpdateAsync(CustomerCategory customerCategory)
    {
        await _repository.UpdateAsync(customerCategory);
    }
}

