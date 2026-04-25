using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IAyarRepository
{
    IQueryable<Ayar> Ayarlar { get; }
    Task<List<Ayar>> GetListAsync();
    Task<Ayar> GetByIdAsync(int ayarId);
    Task<int> InsertAsync(Ayar ayar);
    Task UpdateAsync(Ayar ayar);
    Task DeleteAsync(Ayar ayar);
}
