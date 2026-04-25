using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class InvoiceCategoryRepository : IInvoiceCategoryRepository
{
    private readonly IRepositoryAsync<InvoiceCategory> _repository;

    public InvoiceCategoryRepository(IRepositoryAsync<InvoiceCategory> repository)
    {
        _repository = repository;
    }

    public IQueryable<InvoiceCategory> InvoiceCategories => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(InvoiceCategory invoiceCategory)
    {
        invoiceCategory.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(invoiceCategory);
        //await _repository.DeleteAsync(InvoiceCategory);
    }

    public async Task<InvoiceCategory> GetByIdAsync(int invoiceCategoryId)
    {
        return await _repository.Entities.Where(p => p.Id == invoiceCategoryId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<InvoiceCategory>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(InvoiceCategory invoiceCategory)
    {
        await _repository.AddAsync(invoiceCategory);
        return invoiceCategory.Id;
    }

    public async Task UpdateAsync(InvoiceCategory invoiceCategory)
    {
        await _repository.UpdateAsync(invoiceCategory);
    }
}
