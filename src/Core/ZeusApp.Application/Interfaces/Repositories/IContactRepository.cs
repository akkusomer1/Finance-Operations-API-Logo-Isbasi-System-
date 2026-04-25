using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;
public interface IContactRepository
{
    IQueryable<Contact> Contacts { get; }
    Task<List<Contact>> GetListAsync();
    Task<Contact> GetByIdAsync(int contactId);
    Task<int> InsertAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Contact contact);
}
