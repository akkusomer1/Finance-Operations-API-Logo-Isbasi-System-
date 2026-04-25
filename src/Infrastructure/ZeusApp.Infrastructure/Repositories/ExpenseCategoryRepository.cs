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
public class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly IRepositoryAsync<ExpenseCategory> _repository;

    public ExpenseCategoryRepository(IRepositoryAsync<ExpenseCategory> repository)
    {
        _repository = repository;
    }
    public IQueryable<ExpenseCategory> ExpenseCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);


    public async Task DeleteAsync(ExpenseCategory expenseCategory)
    {
        expenseCategory.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(expenseCategory);
    }

    public async Task<ExpenseCategory> GetByIdAsync(int expenseCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == expenseCategoryId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ExpenseCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ExpenseCategory expenseCategory)
    {
        await _repository.AddAsync(expenseCategory);
        return expenseCategory.Id;
    }

    public async Task UpdateAsync(ExpenseCategory expenseCategory)
    {
        await _repository.UpdateAsync(expenseCategory);
    }
}
