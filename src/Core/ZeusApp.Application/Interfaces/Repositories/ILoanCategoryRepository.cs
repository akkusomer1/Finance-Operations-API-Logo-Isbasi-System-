using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface ILoanCategoryRepository
{
    IQueryable<LoanCategory> LoanCategories { get; }
    Task<List<LoanCategory>> GetListAsync();
    Task<LoanCategory> GetByIdAsync(int loancategoryId);
    Task<int> InsertAsync(LoanCategory loanCategory);
    Task UpdateAsync(LoanCategory loanCategory);
    Task DeleteAsync(LoanCategory loanCategory);
}
