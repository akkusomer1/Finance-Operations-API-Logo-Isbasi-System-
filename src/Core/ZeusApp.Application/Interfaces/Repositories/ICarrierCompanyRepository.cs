using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface ICarrierCompanyRepository
{
    IQueryable<CarrierCompany> CarrierCompanies { get; }
    Task<List<CarrierCompany>> GetListAsync();
    Task<CarrierCompany> GetByIdAsync(int carrierCompanyId);
    Task<int> InsertAsync(CarrierCompany carrierCompany);
    Task UpdateAsync(CarrierCompany carrierCompany);
    Task DeleteAsync(CarrierCompany carrierCompany);
}