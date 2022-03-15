//using Lib.Service.Mongo.Entities;
//using MongoDB.Bson.Serialization.Attributes;

//namespace StoreApi.Core.Domain
//{
//    [BsonCollection("Usuarios")]
//    public class User1
//    {
//        [BsonElement("Name")]
//        public string Name { get; set; }

//        [BsonElement(nameof(Rol))]
//        public string Rol { get; set; }

//        [BsonElement(nameof(UserName))]
//        public string UserName { get; set; }

//        [BsonElement(nameof(Password))]
//        public string Password { get; set; }
//    }
//}
//try
//{
//  var user = await dbProduct.InsertDocument(new User
//  {
//    UserName = request.UserName,
//    PasswordHash = request.PasswordHash,
//    PasswordSalt = request.PasswordSalt,

 // })
