using System;

namespace ZeusApp.Application.Interfaces.Shared;

public interface IDateTimeService
{
    DateTime NowUtc { get; }
}
