using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IOtherAddressRepository
{
    IQueryable<OtherAddress> OtherAddresses { get; }
    Task<List<OtherAddress>> GetListAsync();
    Task<OtherAddress> GetByIdAsync(int otherAddressId);
    Task<int> InsertAsync(OtherAddress otherAddress);
    Task UpdateAsync(OtherAddress otherAddress);
    Task DeleteAsync(OtherAddress otherAddress);
}
