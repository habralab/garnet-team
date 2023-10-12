using HotChocolate.Types;

namespace Garnet.Teams.Infrastructure.Api.TeamCreate
{
    public record TeamCreateInput(string Name, string Description, IFile? File, string[] Tags);
}