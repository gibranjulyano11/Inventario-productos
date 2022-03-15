using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using StoreApi.Core.Modelos;
//using Lib.Service.Mongo.Interfaces;
//using Lib.Service.Mongo.Entities;

namespace StoreApi.Core.Repositorio
{
    //public interface IUserRepositorio<TDocument> where TDocument : IDocument
    public interface IUserRepositorio
    {
        Task<string> Register(Users user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExiste(string username);
    }
}
