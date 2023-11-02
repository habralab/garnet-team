using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application.Args;
using Garnet.Users.Application.Queries;
using Garnet.Users.Infrastructure.Api.UserGet;
using Garnet.Users.Infrastructure.Api.UsersFilter;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Query")]
public class UsersQuery
{
    private readonly UserGetQuery _userGetQuery;
    private readonly UsersFilterQuery _usersFilterQuery;

    public UsersQuery(
        UsersFilterQuery usersFilterQuery,
        UserGetQuery userGetQuery)
    {
        _userGetQuery = userGetQuery;
        _usersFilterQuery = usersFilterQuery;
    }

    public async Task<UserPayload> UserGet(string id)
    {
        var result = await _userGetQuery.Query(id);
        result.ThrowQueryExceptionIfHasErrors();

        var user = result.Value;
        return new UserPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
    }

    public async Task<UsersFilterPayload> UsersFilter(UsersFilterInput input)
    {
        var args = new UserFilterArgs(
            input.Search,
            input.Tags ?? Array.Empty<string>(),
            input.Skip,
            input.Take
        );
        var result = await _usersFilterQuery.Query(args);

        var users = result.Select(o => new UserPayload(o.Id, o.UserName, o.Description, o.AvatarUrl, o.Tags));
        return new UsersFilterPayload(users.ToArray());
    }
}