//using Lib.Service.Mongo.Context;
//using Lib.Service.Mongo.Entities;
//using Lib.Service.Mongo.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using MongoDB.Bson.Serialization.Conventions;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Lib.Service.Mongo
//{
//    public class UserRepositorio<TDocument> : IUserRepositorio<TDocument> where TDocument : IDocument
//    {
//        public readonly IMongoCollection<TDocument> collection;
//        public readonly MongoClient client;

//    private protected static string GetCollectionName(Type documentType)
//    {
//      return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
//    }
//    //private readonly ApplicationDbContext _db;
//    //private readonly IConfiguration _configuration;
//    public UserRepositorio(IOptions<MongoContext> options)
//    {
//      var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
//      ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

//      client = new MongoClient(options.Value.ConnectionString);

//      var db = client.GetDatabase(options.Value.Database);

//      var collectionName = GetCollectionName(typeof(TDocument));

//      collection = db.GetCollection<TDocument>(collectionName);
//    }

//        public async Task<string> Login(string userName, string password)
//        {
//            var user = await _db.Users.FirstOrDefaultAsync(
//                x => x.UserName.ToLower().Equals(userName.ToLower()));

//            if (user == null)
//            {
//                return "nouser";
//            }
//            else if(!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//            {
//                return "wrongpassword";
//            }
//            else
//            {
//                return CrearToken(user);
//            }

//        }

//        public async Task<string> Register(User user, string password)
//        {
//            try
//            {

//                if (await UserExiste(user.UserName))
//                {
//                    return "existe";
//                }

//                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

//                user.PasswordHash = passwordHash;
//                user.PasswordSalt = passwordSalt;

//                await _db.Users.AddAsync(user);
//                await _db.SaveChangesAsync();
//                return CrearToken(user);
//            }
//            catch (Exception)
//            {

//                return "error";
//            }
//        }

//        public async Task<bool> UserExiste(string username)
//        {
//            if (await _db.Users.AnyAsync(x=>x.UserName.ToLower().Equals(username.ToLower())))
//            {
//                return true;
//            }
//            return false;
//        }


//        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using (var hmac = new System.Security.Cryptography.HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//            }
//        }


//        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
//        {
//            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//            {
//                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//                for (int i = 0; i < computedHash.Length; i++)
//                {
//                    if (computedHash[i] != passwordHash[i])
//                    {
//                        return false;
//                    }
//                }
//                return true;
//            }
//        }


//        private string CrearToken(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.UserName)
//            };

//            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
//                                        GetBytes(_configuration.GetSection("AppSettings:Token").Value));

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = System.DateTime.Now.AddDays(1),
//                SigningCredentials = creds
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);

//        }



//    }
//}
