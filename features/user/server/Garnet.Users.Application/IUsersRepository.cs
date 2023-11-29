using Garnet.Users.Application.Args;

namespace Garnet.Users.Application;

public interface IUsersRepository
{
    Task<User?> GetUser(string id);
    Task<User> EditUserDescription(string userId, string description);
    Task<User> EditUsername(string userId, string username);
    Task<User> EditUserTags(string userId, string[] tags);
    Task<User> EditUserAvatar(string userId, string avatarUrl);
    Task<User> CreateUser(string identityId, string username);
    Task<User[]> FilterUsers(UserFilterArgs args);
    Task EditUserTotalScore(string userId, float totalScore);
    Task EditUserSkillScore(string userId, Dictionary<string, float> skillScorePerUser);
    Task CreateIndexes();
}