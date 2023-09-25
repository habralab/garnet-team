using MongoDB.Bson;

namespace Garnet.Common.Infrastructure.Support;

public static class Uuid
{
    public static string NewMongo()
    {
        return ObjectId.GenerateNewId().ToString()!;
    }
    public static string NewGuid()
    {
        return Guid.NewGuid().ToString()!;
    }
}