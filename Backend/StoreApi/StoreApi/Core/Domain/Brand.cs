using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Brands")]
    public class Brand : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
