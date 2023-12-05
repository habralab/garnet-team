namespace Garnet.Teams.Application.TeamUser.Queries
{
    public class TeamUserListByIdQuery
    {
        private readonly ITeamUserRepository _teamUserRepository;

        public TeamUserListByIdQuery(ITeamUserRepository teamUserRepository)
        {
            _teamUserRepository = teamUserRepository;
        }

        public async Task<TeamUserEntity[]> Query(CancellationToken ct, string[] userIds)
        {
            return await _teamUserRepository.TeamUserListByIds(ct, userIds);
        }
    }
}