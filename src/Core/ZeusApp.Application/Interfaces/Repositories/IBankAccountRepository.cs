using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IBankAccountRepository
{
    IQueryable<BankAccount> BankAccounts { get; }
    Task<List<BankAccount>> GetListAsync();
    Task<BankAccount> GetByIdAsync(int bankAccountId);
    Task<int> InsertAsync(BankAccount bankAccount);
    Task UpdateAsync(BankAccount bankAccount);
    Task DeleteAsync(BankAccount bankAccount);
}
