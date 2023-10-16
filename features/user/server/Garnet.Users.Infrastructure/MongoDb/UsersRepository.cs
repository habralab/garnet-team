using Garnet.Common.Application;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using Garnet.Users.Application.Args;
using MongoDB.Driver;

namespace Garnet.Users.Infrastructure.MongoDb;

public class UsersRepository : RepositoryBase, IUsersRepository
{
    private readonly DbFactory _dbFactory;
    private readonly FilterDefinitionBuilder<UserDocument> _f = Builders<UserDocument>.Filter;
    private readonly UpdateDefinitionBuilder<UserDocument> _u = Builders<UserDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<UserDocument> _i = Builders<UserDocument>.IndexKeys;

    public UsersRepository(DbFactory dbFactory, ICurrentUserProvider currentUserProvider, IDateTimeService dateTimeService)
        : base(currentUserProvider, dateTimeService)
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
        var user = await FindOneAndUpdateDocument(
            ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.Description, description)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task<User> EditUserAvatar(CancellationToken ct, string userId, string avatarUrl)
    {
        var db = _dbFactory.Create();
        var user = await FindOneAndUpdateDocument(
            ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.AvatarUrl, avatarUrl)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task<User> CreateUser(CancellationToken ct, string identityId, string username)
    {
        var db = _dbFactory.Create();
        var user = UserDocument.Create(
            new UserDocumentCreateArgs(
               Uuid.NewMongo(),
               identityId,
               username,
               string.Empty,
               string.Empty,
               Array.Empty<string>()
            )
        );
        var userDocument = await InsertOneDocument(ct, db.Users, user);
        return UserDocument.ToDomain(userDocument);
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

    public async Task<User[]> FilterUsers(CancellationToken ct, UserFilterArgs args)
    {
        var db = _dbFactory.Create();

        var searchFilter = args.Search is null
                ? _f.Empty
                : _f.Where(x => x.UserName.ToLower().Contains(args.Search.ToLower()));

        var tagsFilter = args.Tags.Length > 0
            ? _f.All(o => o.Tags, args.Tags)
            : _f.Empty;

        var users =
            await db.Users
                .Find(searchFilter & tagsFilter)
                .Skip(args.Skip)
                .Limit(args.Take)
                .ToListAsync(cancellationToken: ct);
        return users.Select(UserDocument.ToDomain).ToArray();
    }

    public Task<User> EditUserTags(CancellationToken ct, string userId, string[] tags)
    {
        throw new NotImplementedException();
    }
}