using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectFilterByUserParticipantId;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectFilterByUserParticipantId;

[Binding]
public class ProjectFilterByUserParticipantIdSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private ProjectFilterByUserParticipantIdPayload _response;

    public ProjectFilterByUserParticipantIdSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }


    [When(@"пользователь '([^']*)' просматривает список проектов в которых он участвует")]
    public async Task WhenПользовательПросматриваетСписокПроектовВКоторыхОнУчаствует(string username)
    {
        _currentUserProviderFake.LoginAs(username);
        _response = await Query.ProjectFilterByUserParticipantId(CancellationToken.None, _currentUserProviderFake.GetUserIdByUsername(username));
    }


    [Then(@"в списке отображается '([^']*)' проекта")]
    public Task ThenВСпискеОтображаетсяПроект(int projectCount)
    {
        _response.Projects.Count().Should().Be(projectCount);
        return Task.CompletedTask;
    }
}