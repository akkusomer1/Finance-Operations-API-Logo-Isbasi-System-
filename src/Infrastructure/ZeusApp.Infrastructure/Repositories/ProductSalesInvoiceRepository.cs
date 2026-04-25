using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.Infrastructure.Repositories;
public class ProductInvoiceRepository : IProductInvoiceRepository
{
    private readonly IRepositoryAsync<ProductInvoice> _repository;

    public ProductInvoiceRepository(IRepositoryAsync<ProductInvoice> repository)
    {
        _repository = repository;
    }

    public IQueryable<ProductInvoice> ProductInvoices => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(ProductInvoice productInvoice)
    {
        productInvoice.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(productInvoice);
    }

    public async Task<ProductInvoice> GetByIdAsync(int productInvoiceId)
    {
        return await _repository.Entities.Where(p => p.Id == productInvoiceId && p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ProductInvoice>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ProductInvoice productInvoice)
    {
        await _repository.AddAsync(productInvoice);
        return productInvoice.Id;
    }

    public async Task UpdateAsync(ProductInvoice productInvoice)
    {
        await _repository.UpdateAsync(productInvoice);
    }
}
