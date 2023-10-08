using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Application;
using Garnet.Projects.Infrastructure.MongoDb;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamJoinRequestDecide;

[Binding]
public class ProjectTeamJoinRequestDecideSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;


    private readonly FilterDefinitionBuilder<ProjectTeamDocument> _f =
        Builders<ProjectTeamDocument>.Filter;

    private readonly UpdateDefinitionBuilder<ProjectTeamDocument> _u =
        Builders<ProjectTeamDocument>.Update;

    private readonly IProjectTeamJoinRequestRepository _repository;

    public ProjectTeamJoinRequestDecideSteps(StepsArgs args, CurrentUserProviderFake currentUserProviderFake,
        IProjectTeamJoinRequestRepository repository) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _repository = repository;
    }


    [When(@"пользователь '([^']*)' принимает заявку на участие от команды '([^']*)' в проект '([^']*)'")]
    public async Task WhenПользовательПринимаетЗаявкуНаВступлениеВПроект(string username, string teamName,
        string projectName)
    {
    }

    [Then(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task ThenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
    }

    [Then(@"количество заявок на вступление в проект '([^']*)' равно '([^']*)'")]
    public async Task ThenКомандчастникомПроекта(string projectName, int teamJoinRequestsCount)
    {
    }

    [When(@"пользователь '([^']*)' отклоняет заявку на участие от команды '([^']*)' в проект '([^']*)'")]
    public async Task WhenПользовательОтклоняетЗаявкуНаВступлениеВПроект(string username, string teamName,
        string projectName)
    {
    }

    [Then(@"команда '([^']*)' не является участником проекта '([^']*)'")]
    public async Task ThenКомандаНеЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {
    }
}