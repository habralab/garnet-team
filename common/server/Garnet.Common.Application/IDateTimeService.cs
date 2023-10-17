namespace Garnet.Common.Application;

public interface IDateTimeService
{
    public DateTimeOffset UtcNow { get; }
    public DateTimeOffset UtcToday { get; }
}