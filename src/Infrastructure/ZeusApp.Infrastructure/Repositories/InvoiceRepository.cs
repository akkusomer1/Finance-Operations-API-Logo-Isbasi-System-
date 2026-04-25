using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly IRepositoryAsync<Invoice> _repository;

    public InvoiceRepository(IRepositoryAsync<Invoice> repository)
    {
        _repository = repository;
    }

    public IQueryable<Invoice> Invoices => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Invoice invoice)
    {
        invoice.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(invoice);
        //await _repository.DeleteAsync(Invoice);
    }

    public async Task<Invoice> GetByIdAsync(int invoiceId)
    {
        return await _repository.Entities
            .Include(x=>x.CustomerSupplier)
            .Include(x=>x.ProductInvoices)
            .ThenInclude(x=>x.ProductService)
            .Where(p => p.Id == invoiceId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Invoice>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Invoice invoice)
    {
        await _repository.AddAsync(invoice);
        return invoice.Id;
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        await _repository.UpdateAsync(invoice);
    }
}
