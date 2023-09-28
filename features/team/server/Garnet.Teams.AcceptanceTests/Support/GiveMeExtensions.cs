using Garnet.Common.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public static class GiveMeExtensions
    {
        public static UserDocumentBuilder User(this GiveMe _)
        {
            return new UserDocumentBuilder();
        }

        public static TeamDocumentBuilder Team(this GiveMe _)
        {
            return new TeamDocumentBuilder();
        }
    }
}