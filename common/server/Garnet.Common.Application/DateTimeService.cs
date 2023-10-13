namespace Garnet.Common.Application;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    public DateTimeOffset UtcToday => DateTimeOffset.UtcNow.Date;
}