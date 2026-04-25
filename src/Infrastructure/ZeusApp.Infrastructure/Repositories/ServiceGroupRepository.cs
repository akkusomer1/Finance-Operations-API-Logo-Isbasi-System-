using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories;
public class ServiceGroupRepository : IServiceGroupRepository
{
    private readonly IRepositoryAsync<ServiceGroup> _repository;

    public ServiceGroupRepository(IRepositoryAsync<ServiceGroup> repository)
    {
        _repository = repository;
    }

    public IQueryable<ServiceGroup> ServiceGroups  => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);
    

    public async Task DeleteAsync(ServiceGroup serviceGroup)
    {
        serviceGroup.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(serviceGroup);
    }

    public async Task<ServiceGroup> GetByIdAsync(int serviceGroupId)
    {

        return await _repository.Entities.Where(p => p.Id == serviceGroupId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<ServiceGroup>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(ServiceGroup serviceGroup)
    {
        await _repository.AddAsync(serviceGroup);
        return serviceGroup.Id;
    }

    public async Task UpdateAsync(ServiceGroup serviceGroup)
    {
        await _repository.UpdateAsync(serviceGroup);
    }
}
