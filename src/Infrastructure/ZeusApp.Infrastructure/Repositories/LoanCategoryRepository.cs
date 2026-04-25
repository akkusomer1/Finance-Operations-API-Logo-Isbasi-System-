using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;


namespace ZeusApp.Infrastructure.Repositories;
public class LoanCategoryRepository : ILoanCategoryRepository
{
    private readonly IRepositoryAsync<LoanCategory> _repository;

    public LoanCategoryRepository(IRepositoryAsync<LoanCategory> repository)
    {
        _repository = repository;
    }
    public IQueryable<LoanCategory> LoanCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(LoanCategory loanCategory)
    {
        loanCategory.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(loanCategory);
    }

    public async Task<LoanCategory> GetByIdAsync(int loancategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == loancategoryId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<LoanCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(LoanCategory loanCategory)
    {
        await _repository.AddAsync(loanCategory);
        return loanCategory.Id;
    }

    public async Task UpdateAsync(LoanCategory loanCategory)
    {
        await _repository.UpdateAsync(loanCategory);
    }
}
