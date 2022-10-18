using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = null!; 
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Summary { get; set; } = null!;

}

