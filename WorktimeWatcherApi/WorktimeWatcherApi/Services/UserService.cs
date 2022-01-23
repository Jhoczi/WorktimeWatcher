using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorktimeWatcherApi.Models;

namespace WorktimeWatcherApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<WorktimeWatcherDatabaseSettings> worktimeWatcherDatabaseSettings)
    {
        var mongoClient = new MongoClient(worktimeWatcherDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(worktimeWatcherDatabaseSettings.Value.DatabaseName);
        _users = mongoDatabase.GetCollection<User>(worktimeWatcherDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() => await _users.Find(_ => true).ToListAsync();
    public async Task<User> GetAsync(string id) => await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(User newUser) => await _users.InsertOneAsync(newUser);
    public async Task UpdateAsync(string id, User updatedUser) =>
        await _users.ReplaceOneAsync(x => x.Id == id, updatedUser);
    public async Task RemoveAsync(string id) => await _users.DeleteOneAsync(x => x.Id == id);

    public async Task<User?> Verify(User? user)
    {
        if (user is null)
            return null;
        var userResult = await _users.Find(x => x.Login == user.Login && x.Password == user.Password).FirstOrDefaultAsync();
        return userResult ?? null;
    }
        
}