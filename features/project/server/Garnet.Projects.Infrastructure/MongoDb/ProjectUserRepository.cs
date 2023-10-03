﻿using Garnet.Common.Infrastructure.Support;
using Garnet.Projects.Application;
using MongoDB.Driver;

namespace Garnet.Projects.Infrastructure.MongoDb.Migrations;

public class ProjectUserRepository : IProjectUserRepository
{
    private readonly DbFactory _dbFactory;

    public ProjectUserRepository(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ProjectUser> AddUser(CancellationToken ct, string userId)
    {
        var db = _dbFactory.Create();
        var user = ProjectUserDocument.Create(userId);
        await db.ProjectUsers.InsertOneAsync(user, cancellationToken: ct);

        return ProjectUserDocument.ToDomain(user);
    }

    public async Task<ProjectUser?> GetUser(CancellationToken ct, string userId)
    {
        var db = _dbFactory.Create();
        var user = await db.ProjectUsers.Find(x => x.Id == userId).FirstOrDefaultAsync(ct);

        return user is null ? null : ProjectUserDocument.ToDomain(user);
    }
}