using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Contexts;
using Garnet.Teams.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests.CommonSteps
{
    [Binding]
    public class GivenUserStep : BaseSteps
    {
        private readonly GivenUserContext _userContext;

        public GivenUserStep(GivenUserContext userContext, StepsArgs args) : base(args)
        {
            _userContext = userContext;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            _userContext.User = GiveMe.User().WithId(Uuid.NewMongo());
            _userContext.CurrentUserProviderFake.RegisterUser(username, _userContext.User.Id);
            return Task.CompletedTask;
        }
    }
}
