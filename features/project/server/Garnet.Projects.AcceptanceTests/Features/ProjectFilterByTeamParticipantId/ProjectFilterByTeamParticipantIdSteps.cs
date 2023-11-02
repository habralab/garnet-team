using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectFilterByTeamParticipantId;
using Garnet.Projects.Infrastructure.Api.ProjectFilterByUserParticipantId;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectFilterByTeamParticipantId;

[Binding]
public class ProjectFilterByTeamParticipantIdSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectFilterByTeamParticipantIdPayload? _response;

    public ProjectFilterByTeamParticipantIdSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake) :
        base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }


    [When(@"пользователь '([^']*)' просматривает список проектов в которых участвует команда '([^']*)'")]
    public async Task WhenПользовательПросматриваетСписокКомандКоторыеУчаствуютВПроекте(string username,
        string teamName)
    {
        _currentUserProviderFake.LoginAs(username);
        var team = await Db.ProjectTeamsParticipants.Find(x => x.TeamName == teamName).FirstAsync();
        _response = await Query.ProjectFilterByTeamParticipantId(CancellationToken.None, team.TeamId);
    }


    [Then(@"в списке '([^']*)' проекта")]
    public Task ThenВСпискеОтображаетсяПроект(int projectCount)
    {
        _response!.Projects.Count().Should().Be(projectCount);
        return Task.CompletedTask;
    }
}