namespace Garnet.Users.Events;

public record UserCreatedEvent(string UserId, string UserName);