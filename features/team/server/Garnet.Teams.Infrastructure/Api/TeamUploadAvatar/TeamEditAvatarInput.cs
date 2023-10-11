using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api.TeamUploadAvatar
{
    public record TeamUploadAvatarInput(
        string TeamId,
        IFile File
    );
}