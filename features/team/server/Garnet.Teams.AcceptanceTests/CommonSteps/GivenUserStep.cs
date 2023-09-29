using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests.CommonSteps
{
    [Binding]
    public class GivenUserStep : BaseSteps
    {
        private readonly CurrentUserProviderFake _currentUserProviderFake;

        public GivenUserStep(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            var user = GiveMe.User().WithId(Uuid.NewMongo());
            _currentUserProviderFake.RegisterUser(username, user.Id);
            return Task.CompletedTask;
        }
    }
}
