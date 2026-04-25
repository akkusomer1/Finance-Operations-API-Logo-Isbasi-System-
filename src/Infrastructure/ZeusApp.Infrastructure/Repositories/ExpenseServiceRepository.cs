using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;
public class ExpenseServiceRepository : IExpenseServiceRepository
{
    private readonly IRepositoryAsync<ExpenseService> _repository;

    public ExpenseServiceRepository(IRepositoryAsync<ExpenseService> repository)
    {
        _repository = repository;
    }
    public IQueryable<ExpenseService> ExpenseServices => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);
    

    public async Task DeleteAsync(ExpenseService expenseService)
    {
        expenseService.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(expenseService);
    }

    public async Task<ExpenseService> GetByIdAsync(int expenseServiceId)
    {
        return await _repository.Entities.Where(p => p.Id == expenseServiceId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ExpenseService>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ExpenseService expenseService)
    {
        await _repository.AddAsync(expenseService);
        return expenseService.Id;
    }

    public async Task UpdateAsync(ExpenseService expenseService)
    {
        await _repository.UpdateAsync(expenseService);
    }
}
