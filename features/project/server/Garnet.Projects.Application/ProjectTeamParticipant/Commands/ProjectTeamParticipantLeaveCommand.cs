using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.ProjectTeamParticipant.Notifications;

namespace Garnet.Projects.Application.ProjectTeamParticipant.Commands
{
    public class ProjectTeamParticipantLeaveCommand
    {
        private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMessageBus _messageBus;

        public ProjectTeamParticipantLeaveCommand(
            IProjectRepository projectRepository,
            IMessageBus messageBus,
            IProjectTeamParticipantRepository projectTeamParticipantRepository)
        {
            _projectRepository = projectRepository;
            _messageBus = messageBus;
            _projectTeamParticipantRepository = projectTeamParticipantRepository;
        }

        public async Task Execute(CancellationToken ct, string teamId, string projectId)
        {
            var teamParticipant = await _projectTeamParticipantRepository.DeleteProjectTeamParticipantsByTeamIdAndProjectId(ct, teamId, projectId);
            if (teamParticipant is not null)
            {
                var project = await _projectRepository.GetProject(ct, projectId);

                var notification = teamParticipant.CreateTeamLeaveProjectNotification(project!);
                await _messageBus.Publish(notification);
            }
        }
    }
}