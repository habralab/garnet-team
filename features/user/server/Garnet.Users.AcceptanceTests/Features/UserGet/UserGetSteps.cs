using FluentAssertions;
using Garnet.Common.AcceptanceTests.Contexts;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api.UserGet;
using Garnet.Users.Infrastructure.MongoDb;
using HotChocolate.Execution;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Garnet.Users.AcceptanceTests.Features.UserGet;

[Binding]
public class UserGetSteps : BaseSteps
{
    private UserPayload? _response;
    private readonly QueryExceptionsContext _queryExceptionsContext;

    public UserGetSteps(QueryExceptionsContext queryExceptionsContext, StepsArgs args) : base(args)
    {
        _queryExceptionsContext = queryExceptionsContext;
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
            var id = ObjectId.GenerateNewId().ToString();
            await Query.UserGet(CancellationToken.None, id!);
        }
        catch (QueryException ex)
        {
            _queryExceptionsContext.QueryExceptions.Add(ex);
        }
    }

    [Then(@"пользователь получает ошибку '([^']*)'")]
    public Task ThenПользовательПолучаетОшибку(string errorCode)
    {
        var validError = _queryExceptionsContext.QueryExceptions.First().Errors.Any(x => x.Code == errorCode);
        validError.Should().BeTrue();
        return Task.CompletedTask;
    }
}