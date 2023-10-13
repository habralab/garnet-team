using Garnet.Common.Application;

namespace Garnet.Common.AcceptanceTests.Fakes;

public class DateTimeServiceFake : IDateTimeService
{
    public DateTimeOffset UtcNow { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UtcToday { get; set; } = DateTimeOffset.UtcNow.Date;
}