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
public class GeneralBankRepository : IGeneralBankRepository
{
    private readonly IRepositoryAsync<GeneralBank> _repository;

    public GeneralBankRepository(IRepositoryAsync<GeneralBank> repository)
    {
        _repository = repository;
    }
    public IQueryable<GeneralBank> GeneralBanks => _repository.Entities.Include(a=>a.BankAccounts).Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(GeneralBank generalBank)
    {
        generalBank.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(generalBank);
    }

    public async Task<GeneralBank> GetByIdAsync(int generalBankId)
    {
        return await _repository.Entities.Include(a=>a.BankAccounts).Where(p => p.Id == generalBankId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<GeneralBank>> GetListAsync()
    {
        return await _repository.Entities.Include(a=>a.BankAccounts).Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(GeneralBank generalBank)
    {
        await _repository.AddAsync(generalBank);
        return generalBank.Id;
    }

    public async Task UpdateAsync(GeneralBank generalBank)
    {
        await _repository.UpdateAsync(generalBank);
    }
}
