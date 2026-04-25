using System.Threading.Tasks;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Application.Interfaces.Shared;

namespace ZeusApp.Infrastructure.Repositories;

public class LogRepository : ILogRepository
{
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<Audit> _repository;
    private readonly IDateTimeService _dateTimeService;

    public LogRepository(IRepositoryAsync<Audit> repository, IMapper mapper, IDateTimeService dateTimeService)
    {
        _repository = repository;
        _mapper = mapper;
        _dateTimeService = dateTimeService;
    }

    public async Task AddLogAsync(string action, string userId)
    {
        var audit = new Audit()
        {
            Type = action,
            UserId = userId,
            DateTime = _dateTimeService.NowUtc
        };
        await _repository.AddAsync(audit);
    }

}

public class LogProfile : Profile
{
    public LogProfile()
    {
        //CreateMap<AuditLogResponse, Audit>().ReverseMap();
    }
}
