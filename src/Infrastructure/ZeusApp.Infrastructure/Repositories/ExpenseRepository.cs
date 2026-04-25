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
public class ExpenseRepository : IExpenseRepository
{
    private readonly IRepositoryAsync<Expense> _repository;

    public ExpenseRepository(IRepositoryAsync<Expense> repository)
    {
        _repository = repository;
    }
    public IQueryable<Expense> Expenses => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);
    

    public async Task DeleteAsync(Expense expense)
    {
        expense.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(expense);
    }

    public async Task<Expense> GetByIdAsync(int expenseId)
    {
        return _repository
            .Entities
            .Include(x => x.ExpenseCategory)
           .Include(x => x.CustomerSupplier)
           .Where(p => p.Id == expenseId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync()!.Result!;
    }

    public async Task<List<Expense>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Expense expense)
    {
        await _repository.AddAsync(expense);
        return expense.Id;
    }

    public async Task UpdateAsync(Expense expense)
    {
        await _repository.UpdateAsync(expense);
    }
}
