using Garnet.Teams.Application.TeamUser.Args;
using Garnet.Teams.Application.TeamUser.Entities;

namespace Garnet.Teams.Application.TeamUser
{
    public interface ITeamUserRepository
    {
        Task<TeamUserEntity> AddUser(CancellationToken ct, string userId, string username);
        Task<TeamUserEntity?> GetUser(CancellationToken ct, string userId);
        Task<TeamUserEntity?> UpdateUser(CancellationToken ct, string userId, TeamUserUpdateArgs update);
        Task CreateIndexes(CancellationToken ct);
    }
}