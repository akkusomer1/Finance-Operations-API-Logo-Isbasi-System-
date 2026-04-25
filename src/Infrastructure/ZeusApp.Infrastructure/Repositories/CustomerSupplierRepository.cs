using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class CustomerSupplierRepository : ICustomerSupplierRepository
{
    private readonly IRepositoryAsync<CustomerSupplier> _repository;

    public CustomerSupplierRepository(IRepositoryAsync<CustomerSupplier> repository)
    {
        _repository = repository;
    }

    public IQueryable<CustomerSupplier> CustomerSuppliers => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(CustomerSupplier customerSupplier)
    {
        customerSupplier.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(customerSupplier);
        //await _repository.DeleteAsync(Contact);
    }

    public async Task<CustomerSupplier> GetByIdAsync(int customerSupplierId, bool isInclude)
    {
        return isInclude ? _repository.Entities
            .Include(x => x.Contact)
            .Include(x => x.Banks)
            .Include(x => x.OtherAddresses)
            .Include(x => x.RelatedPersons)
            .SingleOrDefault(p => p.Id == customerSupplierId && p.Status != EntityStatus.Deleted)
            :  _repository.Entities.SingleOrDefaultAsync(p => p.Id == customerSupplierId && p.Status != EntityStatus.Deleted).Result;
    }

    public async Task<List<CustomerSupplier>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(CustomerSupplier customerSupplier)
    {
        await _repository.AddAsync(customerSupplier);
        return customerSupplier.Id;
    }

    public async Task UpdateAsync(CustomerSupplier customerSupplier)
    {
        await _repository.UpdateAsync(customerSupplier);
    }
}
