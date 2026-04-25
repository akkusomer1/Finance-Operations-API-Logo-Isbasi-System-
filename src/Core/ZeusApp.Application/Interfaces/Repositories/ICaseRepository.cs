using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface ICaseRepository
{
    IQueryable<Case> Cases { get; }
    Task<List<Case>> GetListAsync();
    Task<Case> GetByIdAsync(int @caseId);
    Task<int> InsertAsync(Case @case);
    Task UpdateAsync(Case @case);
    Task DeleteAsync(Case @case);
}
