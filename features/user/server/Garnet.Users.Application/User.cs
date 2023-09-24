namespace Garnet.Users.Application;

public record User(
    string Id,
    string UserName,
    string Description,
    string[] Tags
);