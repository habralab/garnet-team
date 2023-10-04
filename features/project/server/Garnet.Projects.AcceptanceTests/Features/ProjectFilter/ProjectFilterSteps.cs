using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.AcceptanceTests.Support;
using Garnet.Projects.Infrastructure.MongoDb;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectFilter;

[Binding]
public class ProjectFilterSteps : BaseSteps
{

    public ProjectFilterSteps(StepsArgs args) : base(args)
    {
    }


    [Given(@"существует проект '([^']*)'")]
    public async Task GivenСуществуетПроект(string projectName)
    {
        var user = ProjectUserDocument.Create(Uuid.NewMongo());
        var project = GiveMe.Project().WithProjectName(projectName).WithOwnerUserId(user.Id);
        await Db.Projects.InsertOneAsync(project);
    }

    [Given(@"существует проект '([^']*)' с тегами '([^']*)'")]
    public async Task GivenСуществуетПроектСТегами(string projectName, string tags)
    {

    }

    [When(@"производится поиск проектов по запросу '([^']*)'")]
    public async Task WhenПроизводитсяПоискПроектаПоЗапросу(string query)
    {

    }

    [When(@"производится поиск проектов по тегу '([^']*)'")]
    public async Task WhenПроизводитсяПоискПроектаПоТегу(string tags)
    {

    }

    [Then(@"в списке отображается '([^']*)' проект")]
    public async Task ThenВСпискеОтображаетсяПроект(int projectCount)
    {

    }

}