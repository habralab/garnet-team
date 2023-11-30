using Garnet.Common.AcceptanceTests.Support;

namespace Garnet.Teams.AcceptanceTests.Support
{
    public static class GiveMeExtensions
    {
        public static TeamDocumentBuilder Team(this GiveMe _)
        {
            return new TeamDocumentBuilder();
        }
        
        public static TeamParticipantDocumentBuilder TeamParticipant(this GiveMe _)
        {
            return new TeamParticipantDocumentBuilder();
        }

        public static SendNotificationCommandMessageBuilder SendNotificationCommandMessage(this GiveMe _)
        {
            return new SendNotificationCommandMessageBuilder();
        }
    }
}