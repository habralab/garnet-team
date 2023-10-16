using Garnet.Users.Application;
using Garnet.Users.Application.Commands;
using Garnet.Users.Infrastructure.Api.UserCreate;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditDescription;
using Garnet.Users.Infrastructure.Api.UserEdit.UserUploadAvatar;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class UsersMutation
{
    private readonly UsersService _usersService;
    private readonly UserCreateCommand _userCreateCommand;

    public UsersMutation(
        UsersService usersService,
        UserCreateCommand userCreateCommand)
    {
        _usersService = usersService;
        _userCreateCommand = userCreateCommand;
    }

    public async Task<UserCreatePayload> UserCreate(CancellationToken ct, UserCreateInput input)
    {
        var result = await _userCreateCommand.Execute(ct, input.IdentityId, input.UserName);
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