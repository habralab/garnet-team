using FluentResults;
using Garnet.Teams.Application.TeamProject;

namespace Garnet.Teams.Application.TeamJoinProjectRequest.Commands
{
    public class TeamJoinProjectRequestDecidedCommand
    {
        private readonly ITeamProjectRepository _teamProjectRepository;
        private readonly ITeamJoinProjectRequestRepository _joinProjectRequestRepository;

        public TeamJoinProjectRequestDecidedCommand(ITeamProjectRepository teamProjectRepository, ITeamJoinProjectRequestRepository joinProjectRequestRepository)
        {
            _teamProjectRepository = teamProjectRepository;
            _joinProjectRequestRepository = joinProjectRequestRepository;
        }

        public async Task Execute(CancellationToken ct, string joinProjectRequestId, bool isApproved)
        {
            var request = await _joinProjectRequestRepository.DeleteJoinProjectRequestById(CancellationToken.None, joinProjectRequestId);

            if (isApproved)
            {
                await _teamProjectRepository.AddTeamProject(ct, request!.ProjectId, request.TeamId);
            }
        }
    }
}