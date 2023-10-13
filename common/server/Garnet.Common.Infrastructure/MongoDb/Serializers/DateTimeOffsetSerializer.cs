using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace Garnet.Common.Infrastructure.MongoDb.Serializers;

public static class DateTimeOffsetSerializers
{
    public static DateTimeOffsetSerializer DateTimeInstance { get; } = new(BsonType.DateTime);
}