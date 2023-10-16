using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using Garnet.Users.Application.Queries;
using Garnet.Users.Infrastructure.Api.UserGet;
using Garnet.Users.Infrastructure.Api.UsersFilter;
using HotChocolate.Execution;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Query")]
public class UsersQuery
{
    private readonly UsersService _usersService;
    private readonly UserGetQuery _userGetQuery;

    public UsersQuery(
        UsersService usersService,
        UserGetQuery userGetQuery)
    {
        _usersService = usersService;
        _userGetQuery = userGetQuery;
    }

    public async Task<UserPayload> UserGet(CancellationToken ct, string id)
    {
        var result = await _userGetQuery.Query(ct, id);
        result.ThrowQueryExceptionIfHasErrors();

        var user = result.Value;
        return new UserPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
    }

    public async Task<UsersFilterPayload> UsersFilter(CancellationToken ct, UsersFilterInput input)
    {
        var users = await _usersService.FilterUsers(ct, input.Search, input.Tags ?? Array.Empty<string>(), input.Skip, input.Take);
        return new UsersFilterPayload(users.Select(o => new UserPayload(o.Id, o.UserName, o.Description, o.AvatarUrl, o.Tags)).ToArray());
    }
}