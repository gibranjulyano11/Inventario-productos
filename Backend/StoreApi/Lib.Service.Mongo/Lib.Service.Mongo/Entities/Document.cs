using Lib.Service.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Lib.Service.Mongo.Entities
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; }
        [BsonElement("CreatedDate")]
        [JsonIgnore]
        public DateTime CreatedDateUTC { get; set; } = DateTime.UtcNow;
    }
}
