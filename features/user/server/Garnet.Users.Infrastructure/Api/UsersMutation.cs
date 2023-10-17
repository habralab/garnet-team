using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using Garnet.Users.Application.Commands;
using Garnet.Users.Infrastructure.Api.UserCreate;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditDescription;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditTags;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditUsername;
using Garnet.Users.Infrastructure.Api.UserEdit.UserUploadAvatar;
using HotChocolate.Types;

namespace Garnet.Users.Infrastructure.Api;

[ExtendObjectType("Mutation")]
public class UsersMutation
{
    private readonly UserCreateCommand _userCreateCommand;
    private readonly UserEditDescriptionCommand _userEditDescriptionCommand;
    private readonly UserUploadAvatarCommand _userEditAvatarCommand;
    private readonly UserEditTagsCommand _userEditTagsCommand;

    public UsersMutation(
        UserEditDescriptionCommand userEditDescriptionCommand,
        UserUploadAvatarCommand userEditAvatarCommand,
        UserCreateCommand userCreateCommand,
        UserEditTagsCommand userEditTagsCommand)
    {
        _userCreateCommand = userCreateCommand;
        _userEditAvatarCommand = userEditAvatarCommand;
        _userEditDescriptionCommand = userEditDescriptionCommand;
        _userEditTagsCommand = userEditTagsCommand;
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
            await _userEditAvatarCommand.Execute(
                ct,
                input.File.Name,
                input.File.ContentType,
                input.File.OpenReadStream()
            );
        result.ThrowQueryExceptionIfHasErrors();

        var user = result.Value;
        return new UserUploadAvatarPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
    }

    public async Task<UserEditTagsPayload> UserEditTags(CancellationToken ct, string[] tags)
    {
        var result = await _userEditTagsCommand.Execute(ct, tags);
        result.ThrowQueryExceptionIfHasErrors();

        var user = result.Value;
        return new UserEditTagsPayload(user.Id, user.UserName, user.Description, user.AvatarUrl, user.Tags);
    }

    public Task<UserEditUsernamePayload> UserEditUsername(CancellationToken ct, UserEditUsernameInput input)
    {
        return null;
    }
}