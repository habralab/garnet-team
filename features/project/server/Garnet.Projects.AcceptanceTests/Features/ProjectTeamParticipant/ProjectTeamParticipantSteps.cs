using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Infrastructure.MongoDb;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectTeamParticipant;

[Binding]
public class ProjectTeamParticipantSteps : BaseSteps
{

    public ProjectTeamParticipantSteps(StepsArgs args) : base(args)
    {
    }

    [Given(@"существует команда '([^']*)'")]
    public async Task GivenСуществуетКоманда(string teamName)
    {

    }

    [Given(@"команда '([^']*)' является участником проекта '([^']*)'")]
    public async Task GivenКомандаЯвляетсяУчастникомПроекта(string teamName, string projectName)
    {

    }

    [When(@"происходит получение списка команд-участников проекта '([^']*)'")]
    public async Task WhenПроисходитПолученияСпискаКомандУчастниковПроекта(string projectName)
    {

    }

    [Then(@"количество команд в списке равно '([^']*)'")]
    public async Task ThenКоличествоКомандВСписке(int teamCount)
    {

    }
}