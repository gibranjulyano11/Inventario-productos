using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Lib.Service.Mongo.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
        DateTime CreatedDateUTC { get; }
    }
}
