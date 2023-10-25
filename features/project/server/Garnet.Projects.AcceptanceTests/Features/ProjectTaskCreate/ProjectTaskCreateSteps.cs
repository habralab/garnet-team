using Garnet.Common.AcceptanceTests.Fakes;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTaskCreate;

[Binding]
public class ProjectTaskCreateSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;

    public ProjectTaskCreateSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
    }

    [Given(@"пользователь '(.*)' является участником команды '(.*)'")]
    public async Task ThenПользовательЯвляетсяУчастникомПроекта(string username, string projectName)
    {
    }

    [When(@"пользователь '(.*)' создает в проекте '(.*)' задачу с названием '(.*)'")]
    public async Task WhenПользовательСоздаетВПроектеЗадачу(string username, string projectName, string taskName)
    {
    }

    [Then(@"в системе существует задача с названием '(.*)'")]
    public async Task ThenВСистемеСуществуетЗадача(string taskName)
    {
    }
}