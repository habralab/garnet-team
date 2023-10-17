namespace Garnet.Common.Infrastructure.Support;

public class MissingEnvironmentVariableException : Exception
{
    public MissingEnvironmentVariableException(string environmentVariable) 
        : base($"Missing environment variable '{environmentVariable}'")
    {
    }
}