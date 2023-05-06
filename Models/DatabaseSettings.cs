namespace dotnet_minimal_api.Models;

public class DatabaseSettings
{
  public string ConnectionString = "mongodb://localhost:27017";

  public string DatabaseName = "dotnet_minimal_api";

  // collections
  public string UsersCollectionName = "users";
}