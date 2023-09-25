using FluentAssertions;
using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api.UserEdit;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserEdit;

[Binding]
public class UserEditSteps : BaseSteps
{
    private readonly CurrentUserProviderFake _currentUserProviderFake;

    public UserEditSteps(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
    {
        _currentUserProviderFake = currentUserProviderFake;
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
        await Mutation.UserEditDescription(
            CancellationToken.None,
            _currentUserProviderFake.LoginAs(username),
            new UserEditDescriptionInput(description));
    }

    [Then(@"раздел о себе пользователя '(.*)' состоит из '(.*)'")]
    public async Task ThenРазделОСебеПользователяСостоитИз(string username, string description)
    {
        var user = await Db.Users.Find(o => o.UserName == username).FirstAsync();
        user.Description.Should().Be(description);
    }
}