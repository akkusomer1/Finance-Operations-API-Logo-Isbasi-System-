using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

public class CarrierCompanyRepository : ICarrierCompanyRepository
{
    private readonly IRepositoryAsync<CarrierCompany> _repository;

    public CarrierCompanyRepository(IRepositoryAsync<CarrierCompany> repository)
    {
        _repository = repository;
    }

    public IQueryable<CarrierCompany> CarrierCompanies => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(CarrierCompany carrierCompany)
    {
        await _repository.DeleteAsync(carrierCompany);
    }

    public async Task<CarrierCompany> GetByIdAsync(int carrierCompanyId)
    {
        return await _repository.Entities.Where(p => p.Id == carrierCompanyId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<CarrierCompany>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(CarrierCompany carrierCompany)
    {
        await _repository.AddAsync(carrierCompany);
        return carrierCompany.Id;
    }

    public async Task UpdateAsync(CarrierCompany carrierCompany)
    {
        await _repository.UpdateAsync(carrierCompany);
    }
}