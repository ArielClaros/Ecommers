using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommers.API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("Stock")]
        public int Stock { get; set; }
        [BsonElement("Category")]
        public string? Category { get; set; }
        public int CountInCart { get; set; }
    }
}