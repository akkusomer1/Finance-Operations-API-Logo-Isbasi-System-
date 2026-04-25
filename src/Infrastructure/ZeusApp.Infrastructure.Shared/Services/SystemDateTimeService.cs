using System;
using ZeusApp.Application.Interfaces.Shared;

namespace ZeusApp.Infrastructure.Shared.Services;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
