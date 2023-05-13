namespace dotnet_minimal_api.Models;

public class DatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  // Collections
  public string UsersCollectionName { get; set; } = null!;
}