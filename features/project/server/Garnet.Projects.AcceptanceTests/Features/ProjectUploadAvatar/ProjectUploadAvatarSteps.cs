using System.Text;
using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Projects.Infrastructure.Api.ProjectUploadAvatar;
using Garnet.Projects.Infrastructure.MongoDb.Project;
using HotChocolate.Execution;
using HotChocolate.Types;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace Garnet.Projects.AcceptanceTests.Features.ProjectUploadAvatar;

[Binding]
public class ProjectUploadAvatarSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private QueryExceptionsContext _errorStepContext = null!;
    private readonly RemoteFileStorageFake _fileStorageFake;

    private readonly FilterDefinitionBuilder<ProjectDocument> _f = Builders<ProjectDocument>.Filter;
    private readonly UpdateDefinitionBuilder<ProjectDocument> _u = Builders<ProjectDocument>.Update;

    public ProjectUploadAvatarSteps(
        QueryExceptionsContext errorStepContext,
        CurrentUserProviderFake currentUserProviderFake,
        StepsArgs args, RemoteFileStorageFake fileStorageFake) : base(args)
    {
        _errorStepContext = errorStepContext;
        _currentUserProviderFake = currentUserProviderFake;
        _fileStorageFake = fileStorageFake;
    }

    [Given(@"аватаркой проекта '(.*)' является ссылка '(.*)'")]
    public async Task GivenАватаркойПроектаЯвляетсяСсылка(string projectName, string avatar)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        var avatarUrl = avatar.Replace("ID", project.Id);
        await Db.Projects.UpdateOneAsync(
            _f.Eq(x => x.ProjectName, projectName),
            _u.Set(x => x.AvatarUrl, avatarUrl)
        );
    }

    [When(@"'(.*)' редактирует аватарку проекта '(.*)' на '(.*)'")]
    public async Task WhenПользовательРедактируетАватаркуПроекта(string username, string projectName,
        string avatarFile)
    {
        var claims = _currentUserProviderFake.LoginAs(username);
        var project = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var input = new ProjectUploadAvatarInput(project.Id, new StreamFile(avatarFile,
            () => new MemoryStream(Encoding.Default.GetBytes(avatarFile))
        ));

        try
        {
            await Mutation.ProjectUploadAvatar(CancellationToken.None, claims, input);
        }
        catch (QueryException ex)
        {
            _errorStepContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"аватаркой проекта '(.*)' является ссылка '(.*)'")]
    public async Task ThenАватаркойПроектаЯвляетсяСсылка(string projectName, string avatarUrl)
    {
        var project = await Db.Projects.Find(x => x.ProjectName == projectName).FirstAsync();
        avatarUrl = avatarUrl.Replace("ID", project.Id);
        project.AvatarUrl.Should().Be(avatarUrl);
    }

    [Then(@"в удаленном хранилище для проекта '(.*)' есть файл '(.*)'")]
    public async Task ThenВУдаленномХранилищеДляПроектаЕстьФайл(string projectName, string avatar)
    {
        var user = await Db.Projects.Find(o => o.ProjectName == projectName).FirstAsync();
        var avatarUrl = avatar.Replace("ID", user.Id);
        _fileStorageFake.FilesInStorage.Should().ContainKey(avatarUrl);
    }
}