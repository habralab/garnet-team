using FluentResults;
using Garnet.Common.Application;
using Garnet.Common.Application.MessageBus;
using Garnet.Projects.Application.Project;
using Garnet.Projects.Application.Project.Errors;
using Garnet.Projects.Application.ProjectTask.Args;
using Garnet.Projects.Application.ProjectTask.Errors;
using Garnet.Projects.Application.ProjectTeamParticipant;
using Garnet.Projects.Events.ProjectTask;

namespace Garnet.Projects.Application.ProjectTask.Commands;

public class ProjectTaskCloseCommand
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;
    private readonly IProjectTeamParticipantRepository _projectTeamParticipantRepository;

    public ProjectTaskCloseCommand(
        ICurrentUserProvider currentUserProvider,
        IProjectTaskRepository projectTaskRepository,
        IMessageBus messageBus,
        IProjectRepository projectRepository,
        IProjectTeamParticipantRepository projectTeamParticipantRepository)
    {
        _currentUserProvider = currentUserProvider;
        _projectTaskRepository = projectTaskRepository;
        _messageBus = messageBus;
        _projectRepository = projectRepository;
        _projectTeamParticipantRepository = projectTeamParticipantRepository;
    }

    public async Task<Result<ProjectTaskEntity>> Execute(CancellationToken ct, string taskId)
    {
        var currentUserId = _currentUserProvider.UserId;

        var task = await _projectTaskRepository.GetProjectTaskById(ct, taskId);
        if (task is null)
        {
            return Result.Fail(new ProjectTaskNotFoundError(taskId));
        }

        var project = await _projectRepository.GetProject(ct, task.ProjectId);
        if (project is null)
        {
            return Result.Fail(new ProjectNotFoundError(task.ProjectId));
        }

        if (currentUserId != task.ResponsibleUserId && currentUserId != project.OwnerUserId)
        {
            return Result.Fail(new ProjectTaskResponsiblePersonOnlyCanCloseTaskError());
        }

        if (task.Status == ProjectTaskStatuses.Close)
        {
            return Result.Fail(new ProjectTaskAlreadySetThisStatusError(task.Status));
        }

        RatingCalculation? ratingCalculation = null;

        if (task.Reopened is false)
        {
            var totalScore = (float)Math.Round(9.5 / task.UserExecutorIds.Length * 0.8, 2,
                MidpointRounding.AwayFromZero);
            var skillScore = (float)Math.Round(totalScore / task.Tags.Length, 2, MidpointRounding.AwayFromZero);
            var teamScorePerUser = (float)Math.Round(9.5 / task.UserExecutorIds.Length * 0.2, 2,
                MidpointRounding.AwayFromZero);

            var tags = task.Tags;
            var skillScorePerUser = tags.ToDictionary(x => x, x => skillScore);

            var teams = await _projectTeamParticipantRepository.GetProjectTeamParticipantsByProjectId(ct, project.Id);
            var teamExecutors = teams.Where(x => task.TeamExecutorIds.Contains(x.TeamId)).ToList();

            Dictionary<string, float> teamsScore = new();

            foreach (var team in teamExecutors)
            {
                var users = team.UserParticipants.Where(x => task.UserExecutorIds.Contains(x.Id)).ToList();
                teamsScore[team.TeamId] = users.Count * teamScorePerUser;
            }

            ratingCalculation = new RatingCalculation(
                project.OwnerUserId,
                (float)0.5,
                task.UserExecutorIds,
                totalScore,
                skillScorePerUser,
                teamsScore);
        }

        task = await _projectTaskRepository.CloseProjectTask(ct, taskId, ProjectTaskStatuses.Close);

        await _messageBus.Publish(task.ToClosedEvent() with { RatingCalculation = ratingCalculation });
        return Result.Ok(task);
    }
}