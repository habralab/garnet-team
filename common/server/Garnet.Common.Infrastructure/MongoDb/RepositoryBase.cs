using Garnet.Common.Application;
using MongoDB.Driver;

namespace Garnet.Common.Infrastructure.MongoDb;

public abstract class RepositoryBase
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IDateTimeService _dateTimeService;

    protected RepositoryBase(ICurrentUserProvider currentUserProvider, IDateTimeService dateTimeService)
    {
        _currentUserProvider = currentUserProvider;
        _dateTimeService = dateTimeService;
    }

    protected async Task<TDocument> InsertOneDocument<TDocument>(
        CancellationToken ct,
        IMongoCollection<TDocument> collection,
        TDocument document
    ) where TDocument : DocumentBase
    {
        var creationDate = _dateTimeService.UtcNow;
        var createdUser = _currentUserProvider.UserId;
        var auditInfo = AuditInfo.Create(creationDate, createdUser);
        var createdDocument = document with { AuditInfo = auditInfo };
        await collection.InsertOneAsync(createdDocument, cancellationToken: ct);
        return createdDocument;
    }

    protected async Task<TDocument> FindOneAndUpdateDocument<TDocument>(
        CancellationToken ct,
        IMongoCollection<TDocument> collection,
        FilterDefinition<TDocument> filterDefinition,
        UpdateDefinition<TDocument> updateDefinition
    ) where TDocument : DocumentBase
    {
        var updatedDate = _dateTimeService.UtcNow;
        var updatedUser = _currentUserProvider.UserId;
        var updateDefinitionWithAuditInfo =
            updateDefinition
                .Set(o => o.AuditInfo.UpdatedAt, updatedDate)
                .Set(o => o.AuditInfo.UpdatedBy, updatedUser)
                .Inc(o => o.AuditInfo.Version, 1);
        return await collection.FindOneAndUpdateAsync(
            filterDefinition, 
            updateDefinitionWithAuditInfo,
            options: new FindOneAndUpdateOptions<TDocument>
            {
                ReturnDocument = ReturnDocument.After
            },
            cancellationToken: ct
        );
    }
}