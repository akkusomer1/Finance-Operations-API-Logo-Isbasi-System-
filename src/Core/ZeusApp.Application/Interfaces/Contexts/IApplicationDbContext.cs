using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Interfaces.Contexts;

public interface IApplicationDbContext
{
    #region Connection
    IDbConnection Connection { get; }
    bool HasChanges { get; }
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    #endregion

    DbSet<Ayar> Ayarlar { get; set; }
}
