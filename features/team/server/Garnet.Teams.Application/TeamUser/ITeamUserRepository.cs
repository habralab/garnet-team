using Garnet.Teams.Application.TeamUser.Args;

namespace Garnet.Teams.Application.TeamUser
{
    public interface ITeamUserRepository
    {
        Task<TeamUserEntity> AddUser(CancellationToken ct, TeamUserCreateArgs args);
        Task<TeamUserEntity?> GetUser(CancellationToken ct, string userId);
        Task<TeamUserEntity[]> TeamUserListByIds(CancellationToken ct, string[] userIds);
        Task<TeamUserEntity?> UpdateUser(CancellationToken ct, string userId, TeamUserUpdateArgs update);
    }
}