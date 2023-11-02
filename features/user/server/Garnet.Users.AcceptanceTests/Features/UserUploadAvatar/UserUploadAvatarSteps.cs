using System.Text;
using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Users.Infrastructure.Api.UserEdit.UserUploadAvatar;
using HotChocolate.Types;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserUploadAvatar;

[Binding]
public class UserUploadAvatarSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly RemoteFileStorageFake _fileStorageFake;

    public UserUploadAvatarSteps(
        CurrentUserProviderFake currentUserProviderFake,
        RemoteFileStorageFake fileStorageFake,
        StepsArgs args
    ) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
        _fileStorageFake = fileStorageFake;
    }

    [When(@"пользователь '(.*)' меняет в своем профиле аватарку на '(.*)'")]
    public async Task WhenПользовательМеняетВСвоемПрофилеАватаркуНа(string username, string avatar)
    {
        _currentUserProviderFake.LoginAs(username);
        await Mutation.UserUploadAvatar(
            new UserUploadAvatarInput(
                new StreamFile(
                    avatar,
                    () => new MemoryStream(Encoding.Default.GetBytes(avatar))
                )
            )
        );
    }

    [Then(@"аватарка пользователя '(.*)' является '(.*)'")]
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