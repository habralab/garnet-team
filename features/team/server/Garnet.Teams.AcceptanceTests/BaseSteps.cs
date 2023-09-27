using Garnet.Common.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests
{
    public abstract class BaseSteps
    {
        protected GiveMe GiveMe { get; }

        public BaseSteps(StepsArgs args)
        {
            GiveMe = args.GiveMe;
        }
    }
}
