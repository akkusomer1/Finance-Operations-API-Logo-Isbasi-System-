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
public class UnitRepository : IUnitRepository
{
    private readonly IRepositoryAsync<Unit> _repository;

    public UnitRepository(IRepositoryAsync<Unit> repository)
    {
        _repository = repository;
    }
    public IQueryable<Unit> StockCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public IQueryable<Unit> Units => throw new NotImplementedException();

    public async Task DeleteAsync(Unit unit)
    {
        unit.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(unit);
    }

    public async Task<Unit> GetByIdAsync(int unitId)
    {
        return await _repository.Entities.Where(p => p.Id == unitId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Unit>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Unit unit)
    {
        await _repository.AddAsync(unit);
        return unit.Id;
    }

    public async Task UpdateAsync(Unit unit)
    {
        await _repository.UpdateAsync(unit);
    }
}
