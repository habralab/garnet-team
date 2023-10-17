namespace Garnet.Common.Infrastructure.Support;

public static class EnvironmentEx
{
    public static string GetRequiredEnvironmentVariable(string variable)
    {
        return
            Environment.GetEnvironmentVariable(variable)
            ?? throw new MissingEnvironmentVariableException(variable);
    }
}