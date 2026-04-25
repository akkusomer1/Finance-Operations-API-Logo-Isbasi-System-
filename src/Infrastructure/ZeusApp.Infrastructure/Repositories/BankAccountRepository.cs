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
public class BankAccountRepository : IBankAccountRepository
{
    private readonly IRepositoryAsync<BankAccount> _repository;

    public BankAccountRepository(IRepositoryAsync<BankAccount> repository)
    {
        _repository = repository;
    }
    public IQueryable<BankAccount> BankAccounts => _repository.Entities.Include(a=>a.GeneralBank).Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);


    public async Task DeleteAsync(BankAccount bankAccount)
    {
        bankAccount.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(bankAccount);
    }

    public async Task<BankAccount> GetByIdAsync(int bankAccountId)
    {
        return await _repository.Entities.Where(p => p.Id == bankAccountId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<BankAccount>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(BankAccount bankAccount)
    {
        await _repository.AddAsync(bankAccount);
        return bankAccount.Id;
    }

    public async Task UpdateAsync(BankAccount bankAccount)
    {
        await _repository.UpdateAsync(bankAccount);
    }
}
