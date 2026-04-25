using System;
using System.Threading;
using System.Threading.Tasks;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Application.Interfaces.Shared;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly ApplicationDbContext _dbContext;
    private bool disposed;

    public UnitOfWork(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }

    public Task Rollback()
    {
        //todo
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
        }

        //dispose unmanaged resources
        disposed = true;
    }
}
