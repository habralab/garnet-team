using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api.UserEdit.UserEditDescription;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEditDescription;

[Binding]
public class UserEditDescriptionSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;
    private readonly RemoteFileStorageFake _fileStorageFake;

    public UserEditDescriptionSteps(
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
}