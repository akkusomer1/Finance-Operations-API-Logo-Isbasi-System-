using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface ICustomerSupplierRepository
{
    IQueryable<CustomerSupplier> CustomerSuppliers { get; }
    Task<List<CustomerSupplier>> GetListAsync();
    Task<CustomerSupplier> GetByIdAsync(int customerSupplier,bool isInclude);
    Task<int> InsertAsync(CustomerSupplier customerSupplier);
    Task UpdateAsync(CustomerSupplier customerSupplier);
    Task DeleteAsync(CustomerSupplier customerSupplier);
}
