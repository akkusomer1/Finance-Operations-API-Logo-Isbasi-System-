using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IUnitRepository
{
    IQueryable<Unit> Units { get; }
    Task<List<Unit>> GetListAsync();
    Task<Unit> GetByIdAsync(int unitId);
    Task<int> InsertAsync(Unit unit);
    Task UpdateAsync(Unit unit);
    Task DeleteAsync(Unit unit);
}
