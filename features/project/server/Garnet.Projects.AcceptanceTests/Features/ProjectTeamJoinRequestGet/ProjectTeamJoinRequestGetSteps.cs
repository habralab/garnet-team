using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequestGet;

[Binding]
public class ProjectTeamJoinRequestGetSteps : BaseSteps
{
    public ProjectTeamJoinRequestGetSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"пользователь '([^']*)' является владельцем проекта '([^']*)'")]
    public async Task GivenПользовательЯвляетсяВладельцемПроекта(string username, string projectName)
    {
    }

    [Given(@"существует заявка от команды '([^']*)' на участие в проекте '([^']*)'")]
    public async Task GivenСуществуетЗаявкаОтКомандыНаУчастиеВПроекте(string teamName, string projectName)
    {
    }

    [When(@"пользователь '([^']*)' просматривает заявки проекта '([^']*)'")]
    public async Task WhenПользовательПросматриваетЗаявкиНаВступлениеВПроект(string username, string projectName,
        string teamName)
    {
    }

    [Then(@"в списке заявок отображается '([^']*)' команды")]
    public async Task ThenВСпискеЗаявокОтображаетсяКоманд(int teamJoinRequestCount)
    {
    }

    [Then(@"происходит ошибка 'Просматривать заявки на участие в проекте может только владелец проекта'")]
    public async Task ThenПроисходитОшибка(string errorMsg)
    {
    }
}