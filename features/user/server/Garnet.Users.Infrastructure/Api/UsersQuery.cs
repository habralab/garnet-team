using Garnet.Users.Application;
using Garnet.Users.Infrastructure.Api.UserGet;
using Garnet.Users.Infrastructure.Api.UsersFilter;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Query")]
public class UsersQuery
{
    private readonly UsersService _usersService;

    public UsersQuery(UsersService usersService)
    {
        _usersService = usersService;
    }
    
    public async Task<UserPayload> GetUser(CancellationToken ct, string id)
    {
        var user = await _usersService.GetUser(ct, id)
                   ?? throw new QueryException($"Пользователь с идентификатором '{id}' не найден");
        return new UserPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
    }
    
    public async Task<UsersFilterPayload> UsersFilter(CancellationToken ct, UsersFilterInput input)
    {
        var users = await _usersService.FilterUsers(ct, input.Search, input.Tags ?? Array.Empty<string>(), input.Skip, input.Take);
        return new UsersFilterPayload(users.Select(o => new UserPayload(o.Id, o.UserName, o.Description, o.AvatarUrl, o.Tags)).ToArray());
    }
}