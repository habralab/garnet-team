using System.Text;
using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditDescription;
using Garnet.Users.Infrastructure.Api.UserEdit.UserUploadAvatar;
using HotChocolate.Types;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEdit;

[Binding]
public class UserEditSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly RemoteFileStorageFake _fileStorageFake;

    public UserEditSteps(
        CurrentUserProviderFake currentUserProviderFake, 
        RemoteFileStorageFake fileStorageFake, 
        StepsArgs args
    ) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _fileStorageFake = fileStorageFake;
    }
    
    [Given(@"существует пользователь '([^']*)'")]
    public async Task GivenСуществуетПользователь(string username)
    {
        var id = _currentUserProviderFake.RegisterUser(username, Uuid.NewMongo());
        var user = GiveMe.User().WithId(id).WithUserName(username);
        await Db.Users.InsertOneAsync(user);
    }

    [When(@"пользователь '(.*)' меняет в своем профиле о себе на '(.*)'")]
    public async Task WhenПользовательМеняетВСвоемПрофилеОСебеНа(string username, string description)
    {
        _currentUserProviderFake.LoginAs(username);
        await Mutation.UserEditDescription(
            CancellationToken.None,
            new UserEditDescriptionInput(description));
    }

    [Then(@"раздел о себе пользователя '(.*)' состоит из '(.*)'")]
    public async Task ThenРазделОСебеПользователяСостоитИз(string username, string description)
    {
        var user = await Db.Users.Find(o => o.UserName == username).FirstAsync();
        user.Description.Should().Be(description);
    }

    [When(@"пользователь '(.*)' меняет в своем профиле аватарку на '(.*)'")]
    public async Task WhenПользовательМеняетВСвоемПрофилеАватаркуНа(string username, string avatar)
    {
        _currentUserProviderFake.LoginAs(username);
        await Mutation.UserUploadAvatar(
            CancellationToken.None,
            new UserUploadAvatarInput(
                new StreamFile(
                    avatar, 
                    () => new MemoryStream(Encoding.Default.GetBytes(avatar))
                )
            )
        );
    }

    [Then(@"автарка пользователя '(.*)' является '(.*)'")]
    public async Task ThenАавтаркаПользователяЯвляется(string username, string avatar)
    {
        var user = await Db.Users.Find(o => o.UserName == username).FirstAsync();
        var avatarUrl = avatar.Replace("ID", user.Id);
        user.AvatarUrl.Should().Be(avatarUrl);
    }

    [Then(@"в удаленном хранилище для пользователя '(.*)' есть файл '(.*)'")]
    public async Task ThenВУдаленномХранилищеЕстьФайл(string username, string avatar)
    {
        var user = await Db.Users.Find(o => o.UserName == username).FirstAsync();
        var avatarUrl = avatar.Replace("ID", user.Id);
        _fileStorageFake.FilesInStorage.Should().ContainKey(avatarUrl);
    }
}