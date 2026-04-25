using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IExpenseServiceRepository
{
    IQueryable<ExpenseService> ExpenseServices { get; }
    Task<List<ExpenseService>> GetListAsync();
    Task<ExpenseService> GetByIdAsync(int expenseServiceId);
    Task<int> InsertAsync(ExpenseService expenseService);
    Task UpdateAsync(ExpenseService expenseService);
    Task DeleteAsync(ExpenseService expenseService);
}
