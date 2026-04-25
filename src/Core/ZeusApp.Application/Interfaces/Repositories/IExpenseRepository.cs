using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IExpenseRepository
{
    IQueryable<Expense> Expenses { get; }
    Task<List<Expense>> GetListAsync();
    Task<Expense> GetByIdAsync(int expenseId);
    Task<int> InsertAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Expense expense);
}
