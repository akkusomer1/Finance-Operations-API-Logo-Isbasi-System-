using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface ILoanRepository
{
    IQueryable<Loan> Loans { get; }
    Task<List<Loan>> GetListAsync();
    Task<Loan> GetByIdAsync(int loanId);
    Task<int> InsertAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task DeleteAsync(Loan loan);
}
