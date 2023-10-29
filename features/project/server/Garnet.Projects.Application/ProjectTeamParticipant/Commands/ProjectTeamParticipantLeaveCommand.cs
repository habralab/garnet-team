namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands
{
    public class ProjectTeamParticipantLeaveCommand
    {
        private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

        public ProjectTeamParticipantLeaveCommand(IProjectTeamParticipantRepository projectTeamParticipantRepository)
        {
            _projectTeamParticipantRepository = projectTeamParticipantRepository;
        }

        public async Task Execute(CancellationToken ct, string teamId, string projectId)
        {
            await _projectTeamParticipantRepository.DeleteProjectTeamParticipantsByTeamId(ct, teamId);
        }
    }
}