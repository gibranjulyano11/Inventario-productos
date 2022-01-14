using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Procts")]
    public class Product : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement(nameof(Code))]
        public string Code { get; set; }

        [BsonElement(nameof(Price))]
        public decimal Price { get; set; }

        [BsonElement(nameof(Category))]
        public string Category { get; set; }

        [BsonElement(nameof(Brand))]
        public string Brand { get; set; }

    }

}
