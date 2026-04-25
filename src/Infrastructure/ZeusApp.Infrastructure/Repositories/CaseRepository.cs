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
public class CaseRepository : ICaseRepository
{
    private readonly IRepositoryAsync<Case> _repository;

    public CaseRepository(IRepositoryAsync<Case> repository)
    {
        _repository = repository;
    }

    public IQueryable<Case> Cases => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Case @case)
    {
        @case.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(@case);
    }

    public async Task<Case> GetByIdAsync(int caseId)
    {
        return await _repository.Entities.Where(p => p.Id == caseId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Case>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Case @case)
    {
        await _repository.AddAsync(@case);
        return @case.Id;
    }

    public async Task UpdateAsync(Case @case)
    {
        await _repository.UpdateAsync(@case);
    }
}
