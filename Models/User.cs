using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnet_minimal_api.Models;

public class User
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  [BsonElement("name")]
  public string name { get; set; } = null!;

  [BsonElement("email")]
  public string email { get; set; } = null!;

  [BsonElement("password")]
  public string password { get; set; } = null!;

  [BsonElement("height")]
  public decimal height { get; set; }

  [BsonElement("role")]
  public string role { get; set; } = null!;
}