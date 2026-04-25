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
public class ContactRepository : IContactRepository
{
    private readonly IRepositoryAsync<Contact> _repository;

    public ContactRepository(IRepositoryAsync<Contact> repository)
    {
        _repository = repository;
    }

    public IQueryable<Contact> Contacts => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

    public async Task DeleteAsync(Contact contact)
    {
        contact.Status = EntityStatus.Deleted;
        await _repository.UpdateAsync(contact);
        //await _repository.DeleteAsync(Contact);
    }

    public async Task<Contact> GetByIdAsync(int contactId)
    {
        return await _repository.Entities.Where(p => p.Id == contactId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
    }

    public async Task<List<Contact>> GetListAsync()
    {
        return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
    }

    public async Task<int> InsertAsync(Contact contact)
    {
        await _repository.AddAsync(contact);
        return contact.Id;
    }

    public async Task UpdateAsync(Contact contact)
    {
        await _repository.UpdateAsync(contact);
    }
}
