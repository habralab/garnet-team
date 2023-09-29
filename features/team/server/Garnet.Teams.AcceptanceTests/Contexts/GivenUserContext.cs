using Garnet.Common.AcceptanceTests.Fakes;
using Garnet.Teams.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests.Contexts
{
    public class GivenUserContext
    {
        public GivenUserContext(CurrentUserProviderFake currentUserProviderFake)
        {
            CurrentUserProviderFake = currentUserProviderFake;
            User = new UserDocumentBuilder();
        }

        public UserDocumentBuilder User { get; set; }
        public CurrentUserProviderFake CurrentUserProviderFake { get; private set; }
    }
}
