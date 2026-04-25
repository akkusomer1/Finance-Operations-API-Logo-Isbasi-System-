using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IHoldRepository
{

    IQueryable<Hold> Holds { get; }
    Task<List<Hold>> GetListAsync();
    Task<Hold> GetByIdAsync(int holdId);
    Task<int> InsertAsync(Hold hold);
    Task UpdateAsync(Hold hold);
    Task DeleteAsync(Hold hold);
}