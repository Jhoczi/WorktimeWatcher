using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorktimeWatcherApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("user")] 
    public string Login { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("userRole")]
    public UserType UserType { get; set; }
    
    public User(string? id, string login, string password, UserType userType)
    {
        Id = id;
        Login = login;
        Password = password;
        UserType = userType;
    }
}

public enum UserType
{
    User,
    Admin
}