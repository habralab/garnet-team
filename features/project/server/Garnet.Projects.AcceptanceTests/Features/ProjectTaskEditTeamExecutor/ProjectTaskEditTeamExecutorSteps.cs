using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectTaskEditTeamExecutor;
using Garnet.Projects.Infrastructure.MongoDb.ProjectTask;
using HotChocolate.Execution;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskEditTeamExecutor;

[Binding]
public class ProjectTaskEditTeamExecutorSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext;

    private readonly FilterDefinitionBuilder<ProjectTaskDocument> _f =
        Builders<ProjectTaskDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTaskDocument> _u =
        Builders<ProjectTaskDocument>.Update;

    public ProjectTaskEditTeamExecutorSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        QueryExceptionsContext errorStepContext) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _errorStepContext = errorStepContext;
    }

    [Given(@"команда '(.*)' является исполнителем задачи '(.*)'")]
    public async Task GivenКомандаЯвляетсяИсполнителемЗадачи(string teamName, string taskName)
    {
        var team = await Db.ProjectTeamsParticipants.Find(x => x.TeamName == teamName).FirstAsync();
        await Db.ProjectTasks.UpdateOneAsync(
            _f.Eq(x => x.Name, taskName),
            _u.AddToSet(x => x.TeamExecutorIds, team.TeamId));
    }

    [When(@"пользователь '(.*)' редактирует команду-исполнителя задачи '(.*)' на список команд '(.*)'")]
    public async Task WhenПользовательРедактируетКомандуИсполнителя(string username, string taskName, string teams)
    {
        _currentUserProviderFake.LoginAs(username);
        var teamList = teams.Split(", ");
        var teamIds =
            await Db.ProjectTeams
                .Find(o => teamList.Any(t => o.TeamName == t))
                .Project(o => o.Id)
                .ToListAsync();

        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();
        var input = new ProjectTaskEditTeamExecutorInput(task.Id, teamIds.ToArray());
        try
        {
            await Mutation.ProjectTaskEditTeamExecutor(CancellationToken.None, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"командами-исполнителями задачи '(.*)' являются '(.*)'")]
    public async Task ThenКомандамиИсполнителямиЗадачиЯвляются(string taskName, string teams)
    {
        var teamList = teams.Split(", ");
        var teamIds =
            await Db.ProjectTeamsParticipants
                .Find(o => teamList.Any(t => o.TeamName == t))
                .Project(o => o.TeamId)
                .ToListAsync();

        var task = await Db.ProjectTasks.Find(o => o.Name == taskName).FirstAsync();
        task.TeamExecutorIds.Should().BeEquivalentTo(teamIds);
    }
}