using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface IRelatedPersonRepository
{
    IQueryable<RelatedPerson> RelatedPersons { get; }
    Task<List<RelatedPerson>> GetListAsync();
    Task<RelatedPerson> GetByIdAsync(int relatedPersonId);
    Task<int> InsertAsync(RelatedPerson relatedPerson);
    Task UpdateAsync(RelatedPerson relatedPerson);
    Task DeleteAsync(RelatedPerson relatedPerson);
}
