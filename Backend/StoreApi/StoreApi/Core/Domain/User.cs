using Lib.Service.Mongo.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreApi.Core.Domain
{
    [BsonCollection("Users")]

    public class User : Document
    {
        [BsonElement("UserName")]
        public string UserName { get; set; }
        
        [BsonElement(nameof(Password))]
        public string Password { get; set; }

        [BsonElement(nameof(Role))]
        public string Role { get; set; }
    }
}
