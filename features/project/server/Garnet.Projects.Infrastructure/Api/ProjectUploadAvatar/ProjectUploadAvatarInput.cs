using HotChocolate.Types;

namespace Garnet.Projects.Infrastructure.Api.ProjectUploadAvatar;

public record ProjectUploadAvatarInput(
    string ProjectId,
    IFile File
    );