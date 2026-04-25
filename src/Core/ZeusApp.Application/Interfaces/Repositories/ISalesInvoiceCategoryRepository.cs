using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IInvoiceCategoryRepository
{
    IQueryable<InvoiceCategory> InvoiceCategories { get; }
    Task<List<InvoiceCategory>> GetListAsync();
    Task<InvoiceCategory> GetByIdAsync(int InvoiceCategoryId);
    Task<int> InsertAsync(InvoiceCategory invoiceCategory);
    Task UpdateAsync(InvoiceCategory invoiceCategory);
    Task DeleteAsync(InvoiceCategory invoiceCategory);
}
