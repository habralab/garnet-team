using FluentAssertions;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api;
using Garnet.Users.Infrastructure.Api.UserGet;
using Garnet.Users.Infrastructure.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserGet;

[Binding]
public class UserGetSteps : BaseSteps
{
    private UserPayload? _response;
    private string? _id;
    private Exception? _exception;

    public UserGetSteps(StepsArgs args) : base(args)
    {
    }
    
    [Given(@"существует пользователь '(.*)' с описанием о себе '(.*)'")]
    public async Task GivenСуществуетЗарегистрированныйПользовательСОписаниемОСебе(string username, string description)
    {
        var user = GiveMe.User().WithUserName(username).WithDescription(description);
        await Db.Users.InsertOneAsync(user);
    }

    [When(@"пользователь открывает профиль пользователя '(.*)'")]
    public async Task WhenПользовательОткрываетПрофильПользователя(string username)
    {
        var user = await Db.Users.Find(o => o.UserName == username).FirstAsync();
        _response = await Query.UserGet(CancellationToken.None, user.Id);
    }

    [Then(@"описание о себе открытой карточки пользователя состоит из '(.*)'")]
    public void ThenОписаниеОткрытойКарточкиПользователяСостоитИз(string description)
    {
        _response!.Description.Should().Be(description);
    }

    [When(@"пользователь открывает профиль пользователя, которого нет в системе")]
    public async Task WhenПользовательОткрываетПрофильПользователяКоторогоНетВСистеме()
    {
        try
        {
            _id = ObjectId.GenerateNewId().ToString();
            await Query.UserGet(CancellationToken.None, _id!);
        }
        catch (Exception e)
        {
            _exception = e;
        }
    }

    [Then(@"происходит ошибка '(.*)'")]
    public void ThenПроисходитОшибка(string error)
    {
        var errorMsg = error.Replace("ID", _id);
        _exception!.Message.Should().Be(errorMsg);
    }
}