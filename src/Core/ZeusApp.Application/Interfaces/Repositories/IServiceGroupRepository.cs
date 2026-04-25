using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IServiceGroupRepository
{
    IQueryable<ServiceGroup> ServiceGroups { get; }
    Task<List<ServiceGroup>> GetListAsync();
    Task<ServiceGroup> GetByIdAsync(int serviceGroupId);
    Task<int> InsertAsync(ServiceGroup serviceGroup);
    Task UpdateAsync(ServiceGroup serviceGroup);
    Task DeleteAsync(ServiceGroup serviceGroup);
}
