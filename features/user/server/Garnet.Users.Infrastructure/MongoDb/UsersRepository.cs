using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using MongoDB.Driver;

namespace Garnet.Users.Infrastructure.MongoDb;

public class UsersRepository : IUsersRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<UserDocument> _f = Builders<UserDocument>.Filter;
    private readonly UpdateDefinitionBuilder<UserDocument> _u = Builders<UserDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<UserDocument> _i = Builders<UserDocument>.IndexKeys;

    public UsersRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    
    public async Task<User?> GetUser(CancellationToken ct, string id)
    {
        var db = _dbFactory.Create();
        var user = await db.Users.Find(o => o.Id == id).FirstOrDefaultAsync(ct);
        return user is not null ? UserDocument.ToDomain(user) : null;
    }

    public async Task<User> EditUserDescription(CancellationToken ct, string userId, string description)
    {
        var db = _dbFactory.Create();
        var user =
            await db.Users.FindOneAndUpdateAsync(
                _f.Eq(o => o.Id, userId),
                _u.Set(o => o.Description, description),
                options: new FindOneAndUpdateOptions<UserDocument>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken: ct);
        return UserDocument.ToDomain(user);
    }

    public async Task<User> EditUserAvatar(CancellationToken ct, string userId, string avatarUrl)
    {
        var db = _dbFactory.Create();
        var user =
            await db.Users.FindOneAndUpdateAsync(
                _f.Eq(o => o.Id, userId),
                _u.Set(o => o.AvatarUrl, avatarUrl),
                options: new FindOneAndUpdateOptions<UserDocument>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken: ct);
        return UserDocument.ToDomain(user);
    }

    public async Task<User> CreateUser(CancellationToken ct, string identityId, string username)
    {
        var db = _dbFactory.Create();
        var user = UserDocument.Create(
            Uuid.NewMongo(),
            identityId,
            username,
            string.Empty,
            Array.Empty<string>());
        await db.Users.InsertOneAsync(user, cancellationToken: ct);
        return UserDocument.ToDomain(user);
    }

    public async Task<User[]> FilterUsers(CancellationToken ct, string? search, string[] tags, int skip, int take)
    {
        var db = _dbFactory.Create();

        var searchFilter =
            search is null
                ? _f.Empty
                : _f.Text(search);

        var tagsFilter =
            tags.Length > 0
                ? _f.All(o => o.Tags, tags)
                : _f.Empty;
        
        var users =
            await db.Users
                .Find(searchFilter & tagsFilter)
                .Skip(skip)
                .Limit(take)
                .ToListAsync(cancellationToken: ct);
        return users.Select(UserDocument.ToDomain).ToArray();
    }

    public async Task CreateIndexes(CancellationToken ct)
    {
        var db = _dbFactory.Create();
        await db.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDocument>(
                _i.Text(o => o.UserName)
                    .Text(o => o.Description)
                    .Text(o => o.Tags)
            ),
            cancellationToken: ct);
    }
}