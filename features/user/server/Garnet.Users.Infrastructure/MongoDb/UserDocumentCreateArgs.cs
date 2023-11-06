namespace Garnet.Users.Infrastructure.MongoDb;

public record UserDocumentCreateArgs(
    string Id,
    string UserName, 
    string Description, 
    string AvatarUrl, 
    string[] Tags);