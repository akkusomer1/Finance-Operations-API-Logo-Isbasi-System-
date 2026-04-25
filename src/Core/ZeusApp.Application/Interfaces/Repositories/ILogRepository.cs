using System.Threading.Tasks;

namespace ZeusApp.Application.Interfaces.Repositories;

public interface ILogRepository
{
    Task AddLogAsync(string action, string userId);
}
