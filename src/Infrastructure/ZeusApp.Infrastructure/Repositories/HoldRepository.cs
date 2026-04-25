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
public class HoldRepository : IHoldRepository
{
    private readonly IRepositoryAsync<Hold> _repository;

    public HoldRepository(IRepositoryAsync<Hold> repository)
    {
        _repository = repository;
    }

    public IQueryable<Hold> Holds => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Hold hold)
    {
        await _repository.DeleteAsync(hold);
    }

    public async Task<Hold> GetByIdAsync(int holdId)
    {
        return await _repository.Entities.Where(p => p.Id == holdId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Hold>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Hold hold)
    {
        await _repository.AddAsync(hold);
        return hold.Id;
    }

    public async Task UpdateAsync(Hold hold)
    {
        await _repository.UpdateAsync(hold);
    }
}
