using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;
public class LoanRepository : ILoanRepository
{
    private readonly IRepositoryAsync<Loan> _repository;

    public LoanRepository(IRepositoryAsync<Loan> repository)
    {
        _repository = repository;
    }

    public IQueryable<Loan> Loans => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Loan loan)
    {
        loan.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(loan);
    }

    public async Task<Loan> GetByIdAsync(int loanId)
    {
        return await _repository.Entities.Where(p => p.Id == loanId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Loan>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Loan loan)
    {
        await _repository.AddAsync(loan);
        return loan.Id;
    }

    public async Task UpdateAsync(Loan loan)
    {
        await _repository.UpdateAsync(loan);
    }
}
