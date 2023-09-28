using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Common.Infrastructure.Support;
using Garnet.Teams.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests
{
    public abstract class BaseStepsWithGivenUser : BaseSteps
    {
        protected readonly CurrentUserProviderFake _currentUserProviderFake;
        protected UserDocumentBuilder _user = null!;
        protected BaseStepsWithGivenUser(CurrentUserProviderFake currentUserProviderFake, StepsArgs args) : base(args)
        {
            _currentUserProviderFake = currentUserProviderFake;
        }

        [Given(@"существует пользователь '([^']*)'")]
        public Task GivenСуществуетПользователь(string username)
        {
            _user = GiveMe.User().WithId(Uuid.NewMongo());
            _currentUserProviderFake.RegisterUser(username, _user.Id);
            return Task.CompletedTask;
        }
    }
}
