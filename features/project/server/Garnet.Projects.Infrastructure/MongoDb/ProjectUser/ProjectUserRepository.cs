using Garnet.Projects.Application.ProjectUser;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.ProjectUser;

public class ProjectUserRepository : IProjectUserRepository
{
    private readonly DbFactory _dbFactory;

    private readonly UpdateDefinitionBuilder<ProjectUserDocument> _u =
        Builders<ProjectUserDocument>.Update;

    private readonly FilterDefinitionBuilder<ProjectUserDocument> _userFilter =
        Builders<ProjectUserDocument>.Filter;


    public ProjectUserRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectUserEntity> AddUser(CancellationToken ct, string userId, string userName)
    {
        var db = _dbFactory.Create();
        var user = ProjectUserDocument.Create(userId, userName, null!);
        await db.ProjectUsers.InsertOneAsync(user, cancellationToken: ct);

        return ProjectUserDocument.ToDomain(user);
    }

    public async Task UpdateUser(CancellationToken ct, string userId, string userName, string userAvatarUrl)
    {
        var db = _dbFactory.Create();
        await db.ProjectUsers.FindOneAndUpdateAsync(
            _userFilter.Eq(x => x.Id, userId),
            _u.Set(x => x.UserName, userName)
                .Set(x => x.UserAvatarUrl, userAvatarUrl),
            cancellationToken: ct
        );
    }

    public async Task<ProjectUserEntity?> GetUser(CancellationToken ct, string userId)
    {
        var db = _dbFactory.Create();
        var user = await db.ProjectUsers.Find(x => x.Id == userId).FirstOrDefaultAsync(ct);

        return user is null ? null : ProjectUserDocument.ToDomain(user);
    }
}