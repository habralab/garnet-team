using Garnet.Common.AcceptanceTests.Support;

namespace Garnet.Notifications.AcceptanceTests
{
    public class StepsArgs
    {
        public GiveMe GiveMe { get; }

        public StepsArgs(GiveMe giveMe)
        {
            GiveMe = giveMe;
        }
    }
}