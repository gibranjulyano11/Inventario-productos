using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Attributes")]
    public class Attribute : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
