using FluentAssertions;
using Garnet.Users.AcceptanceTests.Support;
using Garnet.Users.Infrastructure.Api.UsersFilter;

namespace Garnet.Users.AcceptanceTests.Features.UsersFilter;

[Binding]
public class UsersFilterSteps : BaseSteps
{
    private UsersFilterPayload? _result;

    public UsersFilterSteps(StepsArgs args) : base(args)
    {
    }
    
    [Given(@"существует пользователь '(.*)' с тегами '(.*)'")]
    public async Task GivenСуществуетПользовательСТегами(string username, string tags)
    {
        var tagsArray = tags.Split(", ");
        var user = GiveMe.User().WithUserName(username).WithTags(tagsArray);
        await Db.Users.InsertOneAsync(user);
    }

    [When(@"производится поиск пользователей с запросом '(.*)'")]
    public async Task WhenПроизводитсяПоискПользователейСЗапросом(string search)
    {
        _result = await Query.UsersFilter(new UsersFilterInput(search, null, 0, 100));
    }

    [Then(@"в списке отображается '(.*)' пользователь")]
    public void ThenВСпискеОтображаетсяПользователь(int count)
    {
        _result!.Users.Should().HaveCount(count);
    }

    [When(@"производится поиск пользователей по тегам '(.*)'")]
    public async Task WhenПроизводитсяПоискПользователейПоТегам(string tags)
    {
        var tagsArray = tags.Split(", ");
        _result = await Query.UsersFilter(new UsersFilterInput(null, tagsArray, 0, 100));
    }

    [Then(@"в списке у первого пользователя в имени присутсвтует '(.*)'")]
    public void ThenВСпискеУПервогоПользователяВИмениПрисутсвтует(string text)
    {
        _result!.Users.First().UserName.Should().Contain(text);
    }

    [Then(@"в списке у первого пользователя в навыках присутствует '(.*)'")]
    public void ThenВСпискеУПервогоПользователяВНавыкахПрисутствует(string sql)
    {
        _result!.Users.First().Tags.Should().Contain(sql);
    }
}