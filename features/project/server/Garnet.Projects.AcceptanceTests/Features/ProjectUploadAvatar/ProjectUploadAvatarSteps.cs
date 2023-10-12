using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectEdit;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectUploadAvatar;


[Binding]
public class ProjectUploadAvatarSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext = null!;

    public ProjectUploadAvatarSteps(QueryExceptionsContext errorStepContext, CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args, ProjectEditDescriptionPayload? response) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
    }

    [Given(@"аватаркой проекта '(.*)' является ссылка '(.*)'")]
    public async Task GivenАватаркойПроектаЯвляетсяСсылка(string projectName, string avatarUrl)
    {

    }

    [When(@"'(.*)' редактирует аватарку проекта '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетАватаркуПроекта(string username, string projectName,
        string avatarFile)
    {

    }

    [Then(@"аватарка проекта '(.*)' является '(.*)'")]
    public async Task ThenАватаркоаПроектаЯвляется(string projectName, string avatarUrl)
    {

    }

    [Then(@"в удаленном хранилище для проекта '(.*)' есть файл '(.*)'")]
    public async Task ThenВУдаленномХранилищеДляПроектаЕстьФайл(string projectName, string avatarUrl)
    {

    }

    [Then(@"в системе аватарка проекта '(.*)' содержит ссылку '(.*)'")]
    public async Task ThenВСистемеАватаркаПроектаСодержитСсылку(string projectName, string avatarUrl)
    {

    }
}