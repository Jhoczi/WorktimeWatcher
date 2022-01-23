using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorktimeWatcherApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
}