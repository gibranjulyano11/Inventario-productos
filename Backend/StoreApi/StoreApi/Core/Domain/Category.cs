using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Categories")]
    public class Category : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
