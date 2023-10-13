using System.Security.Claims;
using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Identity;
using Garnet.Users.Application;
using Garnet.Users.Infrastructure.Api.UserCreate;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditDescription;
using Garnet.Users.Infrastructure.Api.UserEdit.UserUploadAvatar;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class UsersMutation
{
    private readonly UsersService _usersService;

    public UsersMutation(UsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<UserCreatePayload> UserCreate(CancellationToken ct, UserCreateInput input)
    {
        var result = await _usersService.CreateUser(ct, input.IdentityId, input.UserName);
        return new UserCreatePayload(result.Id, result.UserName, result.Description, result.AvatarUrl, result.Tags);
    }
    
    public async Task<UserEditDescriptionPayload> UserEditDescription(CancellationToken ct, UserEditDescriptionInput input)
    {
        var result = await _usersService.EditCurrentUserDescription(ct, input.Description);
        return new UserEditDescriptionPayload(result.Id, result.UserName, result.Description, result.AvatarUrl, result.Tags);
    }
    
    public async Task<UserUploadAvatarPayload> UserUploadAvatar(CancellationToken ct, UserUploadAvatarInput input)
    {
        var result =
            await _usersService.EditCurrentUserAvatar(
                ct,
                input.File.Name,
                input.File.ContentType,
                input.File.OpenReadStream()
            );
        return new UserUploadAvatarPayload(result.Id, result.UserName, result.Description, result.AvatarUrl, result.Tags);
    }
}