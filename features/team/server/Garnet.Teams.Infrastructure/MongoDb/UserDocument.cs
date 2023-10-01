namespace Garnet.Teams.Infrastructure.MongoDb
{
    public record UserDocument
    {
        public string Id { get; init; } = null!;
        public string UserId { get; init; } = null!;

        public static UserDocument Create(string id, string userId)
        {
            return new UserDocument
            {
                Id = id,
                UserId = userId
            };
        }

        public static string ToDomain(UserDocument doc)
        {
            return doc.UserId;
        }
    }
}