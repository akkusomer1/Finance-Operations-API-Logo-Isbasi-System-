using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IGeneralBankRepository
{
    IQueryable<GeneralBank> GeneralBanks { get; }
    Task<List<GeneralBank>> GetListAsync();
    Task<GeneralBank> GetByIdAsync(int generalBankId);
    Task<int> InsertAsync(GeneralBank generalBank);
    Task UpdateAsync(GeneralBank generalBank);
    Task DeleteAsync(GeneralBank generalBank);
}
