using Garnet.Common.Infrastructure.Support;
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
    private readonly UserEditDescriptionCommand _userEditDescriptionCommand;

    public UsersMutation(
        UsersService usersService,
        UserEditDescriptionCommand userEditDescriptionCommand,
        UserCreateCommand userCreateCommand)
    {
        _usersService = usersService;
        _userCreateCommand = userCreateCommand;
        _userEditDescriptionCommand = userEditDescriptionCommand;
    }

    public async Task<UserCreatePayload> UserCreate(CancellationToken ct, UserCreateInput input)
    {
        var result = await _userCreateCommand.Execute(ct, input.IdentityId, input.UserName);
        return new UserCreatePayload(result.Id, result.UserName, result.Description, result.AvatarUrl, result.Tags);
    }

    public async Task<UserEditDescriptionPayload> UserEditDescription(CancellationToken ct, UserEditDescriptionInput input)
    {
        var result = await _userEditDescriptionCommand.Execute(ct, input.Description);
        result.ThrowQueryExceptionIfHasErrors();

        var user = result.Value;
        return new UserEditDescriptionPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
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