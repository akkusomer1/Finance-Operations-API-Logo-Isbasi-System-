using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IInvoiceRepository
{
    IQueryable<Invoice> Invoices { get; }
    Task<List<Invoice>> GetListAsync();
    Task<Invoice> GetByIdAsync(int invoiceId);
    Task<int> InsertAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
}
