using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Garnet.Common.Infrastructure.Identity.Kratos.Errors;
using Garnet.Common.Infrastructure.Support;
using HttpMethod = System.Net.Http.HttpMethod;
using Result = FluentResults.Result;

namespace Garnet.Common.Infrastructure.Identity.Kratos;

public class KratosProvider
{
    private readonly HttpClient _kratosHttpClient = new()
    {
        BaseAddress = new Uri(EnvironmentEx.GetRequiredEnvironmentVariable("KRATOS_URL"))
    };

    public async Task<FluentResults.Result<string>> GetUserIdentityIdByCookie(CancellationToken ct, string sessionCookieName, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Fail(new MissingAuthTokenError("Отсутствует авторизационный токен в cookies"));
        }
        
        var request = new HttpRequestMessage(HttpMethod.Get, "sessions/whoami");
        request.Headers.Add(HttpRequestHeader.Cookie.ToString("G"), $"{sessionCookieName}={token}");
        return await SendWhoamiRequestAsync(request);
    }

    public async Task<FluentResults.Result<string>> GetUserIdentityIdByToken(CancellationToken ct, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Fail(new MissingAuthTokenError("Отсутствует авторизационный токен"));
        }
        
        var request = new HttpRequestMessage(HttpMethod.Get, "sessions/whoami");
        request.Headers.Add(HttpRequestHeader.Authorization.ToString("G"), token);
        return await SendWhoamiRequestAsync(request);
    }

    private async Task<string> SendWhoamiRequestAsync(HttpRequestMessage request)
    {
        var res = await _kratosHttpClient.SendAsync(request);
        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadAsStringAsync();
        var whoami = JsonSerializer.Deserialize<Whoami>(json);
        if (!whoami!.Active)
        {
            throw new InvalidOperationException("Сессия не активна");
        }

        return whoami.Identity.Id;
    }
    
    private class Whoami
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } 

        [JsonPropertyName("active")]
        public bool Active { get; set; } 

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; } 

        [JsonPropertyName("authenticated_at")]
        public DateTime AuthenticatedAt { get; set; } 

        [JsonPropertyName("issued_at")]
        public DateTime IssuedAt { get; set; } 

        [JsonPropertyName("identity")]
        public Identity Identity { get; set; } 
    }

    private class Identity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}