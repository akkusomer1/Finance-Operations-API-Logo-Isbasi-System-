using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IProductInvoiceRepository
{
    IQueryable<ProductInvoice> ProductInvoices { get; }
    Task<List<ProductInvoice>> GetListAsync();
    Task<ProductInvoice> GetByIdAsync(int productInvoiceId);
    Task<int> InsertAsync(ProductInvoice productInvoice);
    Task UpdateAsync(ProductInvoice productInvoice);
    Task DeleteAsync(ProductInvoice productInvoice);
}
