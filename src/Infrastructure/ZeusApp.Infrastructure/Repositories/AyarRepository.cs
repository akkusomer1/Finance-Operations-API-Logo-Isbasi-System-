using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class AyarRepository : IAyarRepository
{
    private readonly IRepositoryAsync<Ayar> _repository;

    public AyarRepository(IRepositoryAsync<Ayar> repository)
    {
        _repository = repository;
    }

    public IQueryable<Ayar> Ayarlar => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Ayar ayar)
    {
        ayar.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(ayar);
        //await _repository.DeleteAsync(ayar);
    }

    public async Task<Ayar> GetByIdAsync(int ayarId)
    {
        return await _repository.Entities.Where(p => p.Id == ayarId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Ayar>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Ayar ayar)
    {
        await _repository.AddAsync(ayar);
        return ayar.Id;
    }

    public async Task UpdateAsync(Ayar ayar)
    {
        await _repository.UpdateAsync(ayar);
    }
}
