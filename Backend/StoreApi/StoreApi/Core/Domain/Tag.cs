using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Tags")]
    public class Tag : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}