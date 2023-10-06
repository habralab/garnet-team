using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequest;

[Binding]
public class ProjectTeamJoinRequestSteps : BaseSteps
{
    public ProjectTeamJoinRequestSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"пользователь '([^']*)' является владельцем команды '([^']*)'")]
    public async Task GivenПользовательЯвляетсяВладельцемКоманды(string username, string teamName)
    {

    }

    [When(@"пользователь '([^']*)' отправляет заявку на вступление в проект '([^']*)' от лица команды '([^']*)'")]
    public async Task WhenПользовательОтправляетЗаявкуНаВступлениеВПроект(string username, string projectName , string teamName)
    {

    }

    [Then(@" проекте '([^']*)' существует заявка на вступление от команды '([^']*)'")]
    public async Task ThenВПроектеСуществуетЗаявкаНаВступлениеОтКоманды(int teamCount)
    {

    }
}