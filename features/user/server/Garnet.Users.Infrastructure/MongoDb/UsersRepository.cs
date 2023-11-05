using Garnet.Common.Application;
using Garnet.Common.Infrastructure.Api.Cancellation;
using Garnet.Common.Infrastructure.MongoDb;
using Garnet.Common.Infrastructure.Support;
using Garnet.Users.Application;
using Garnet.Users.Application.Args;
using MongoDB.Driver;

namespace Garnet.Users.Infrastructure.MongoDb;

public class UsersRepository : RepositoryBase, IUsersRepository
{
    private readonly DbFactory _dbFactory;
    private readonly CancellationToken _ct;
    private readonly FilterDefinitionBuilder<UserDocument> _f = Builders<UserDocument>.Filter;
    private readonly UpdateDefinitionBuilder<UserDocument> _u = Builders<UserDocument>.Update;
    private readonly IndexKeysDefinitionBuilder<UserDocument> _i = Builders<UserDocument>.IndexKeys;

    public UsersRepository(
        DbFactory dbFactory,
        ICurrentUserProvider currentUserProvider,
        IDateTimeService dateTimeService,
        CancellationTokenProvider ctp
    )
        : base(currentUserProvider, dateTimeService)
    {
        _dbFactory = dbFactory;
        _ct = ctp.Token;
    }

    public async Task<User?> GetUser(string id)
    {
        var db = _dbFactory.Create();
        var user = await db.Users.Find(o => o.Id == id).FirstOrDefaultAsync(_ct);
        return user is not null ? UserDocument.ToDomain(user) : null;
    }

    public async Task<User> EditUserDescription(string userId, string description)
    {
        var db = _dbFactory.Create();
        var user = await FindOneAndUpdateDocument(
            _ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.Description, description)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task<User> EditUserAvatar(string userId, string avatarUrl)
    {
        var db = _dbFactory.Create();
        var user = await FindOneAndUpdateDocument(
            _ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.AvatarUrl, avatarUrl)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task<User> CreateUser(string identityId, string username)
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
        var userDocument = await InsertOneDocument(_ct, db.Users, user);
        return UserDocument.ToDomain(userDocument);
    }

    public async Task<User[]> FilterUsers(UserFilterArgs args)
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
                .ToListAsync(cancellationToken: _ct);
        return users.Select(UserDocument.ToDomain).ToArray();
    }

    public async Task<User> EditUserTags(string userId, string[] tags)
    {
        var db = _dbFactory.Create();
        var user = await FindOneAndUpdateDocument(
            _ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.Tags, tags)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task<User> EditUsername(string userId, string username)
    {
        var db = _dbFactory.Create();
        var user = await FindOneAndUpdateDocument(
            _ct,
            db.Users,
            _f.Eq(o => o.Id, userId),
            _u.Set(o => o.UserName, username)
        );
        return UserDocument.ToDomain(user);
    }

    public async Task CreateIndexes()
    {
        var db = _dbFactory.Create();
        await db.Users.Indexes.CreateOneAsync(
            new CreateIndexModel<UserDocument>(
                _i.Text(o => o.UserName)
                    .Text(o => o.Description)
                    .Text(o => o.Tags)
            ),
            cancellationToken: _ct);
    }
}