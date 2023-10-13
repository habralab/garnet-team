using Garnet.Common.Infrastructure.MongoDb.Serializers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Garnet.Common.Infrastructure.MongoDb;

public static class Startup
{
    public static IServiceCollection AddGarnetMongoSerializers(this IServiceCollection services)
    {
        BsonSerializer.TryRegisterSerializer(
            typeof(DateTimeOffset),
            DateTimeOffsetSerializers.DateTimeInstance
        );
        return services;
    }
}