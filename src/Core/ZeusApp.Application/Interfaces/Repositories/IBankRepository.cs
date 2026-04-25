using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IBankRepository
{
    IQueryable<Bank> Banks { get; }
    Task<List<Bank>> GetListAsync();
    Task<Bank> GetByIdAsync(int bankId);
    Task<int> InsertAsync(Bank bank);
    Task UpdateAsync(Bank bank);
    Task DeleteAsync(Bank bank);
}
