namespace Garnet.Users.Events;

public record UserUpdatedEvent(
    string UserId, 
    string UserName,
    string Description,
    string AvatarUrl,
    string[] Tags
);