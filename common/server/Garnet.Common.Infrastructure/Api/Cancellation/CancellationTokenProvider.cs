using Microsoft.AspNetCore.Http;

namespace Garnet.Common.Infrastructure.Api.Cancellation;

public class CancellationTokenProvider
{
    public CancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        Token = httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
    
    public CancellationToken Token { get; }
}