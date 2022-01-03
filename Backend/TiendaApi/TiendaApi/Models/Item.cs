using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TiendaApi.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string ItemName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string brands { get; set; } = null!;

        public string data { get; set; } = null!;

        public string tags { get; set; } = null!; 
    }
}
