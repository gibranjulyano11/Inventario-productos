//using Lib.Service.Mongo.Entities;
//using System.Threading.Tasks;
//using Microsoft.Azure.Documents;
//using User = Lib.Service.Mongo.Entities.User;
//using Microsoft.EntityFrameworkCore;

//namespace Lib.Service.Mongo.Interfaces
//{
//    public interface IUserRepositorio<TDocument> where TDocument : IDocument
//    {
//        Task<string> Register(User user, string password);
//        Task<string> Login(string userName, string password);
//        Task<bool> UserExiste(string username);
//        public DbSet<User> Users { get; set; }

//  }
//}
