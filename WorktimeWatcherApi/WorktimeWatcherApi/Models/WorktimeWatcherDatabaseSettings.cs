namespace WorktimeWatcherApi.Models;

public class WorktimeWatcherDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
}