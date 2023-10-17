using Garnet.Common.Infrastructure.MongoDb.Migrations;
using Garnet.Users.Application;

namespace Garnet.Users.Infrastructure.MongoDb.Migrations;

public class CreateIndexesMigration : IRepeatableMigration
{
    private readonly IUsersRepository _usersRepository;

    public CreateIndexesMigration(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task Execute(CancellationToken ct)
    {
        await _usersRepository.CreateIndexes(ct);
    }
}