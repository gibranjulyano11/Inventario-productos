//using StoreApi.Core.Domain;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using StoreApi.Data;
//using MediatR;
//using FluentValidation;
//using Lib.Service.Mongo.Interfaces;
//using System.Threading;

//namespace StoreApi.Core.Repositorio
//{
//  public class UserCreate
//  {
//      public class UserCreateCommand : IRequest<string>
//      {
//        public int Id { get; set; }

//        public string UserName { get; set; }

//        public byte[] PasswordHash { get; set; }

//        public byte[] PasswordSalt { get; set; }

//      }
//    public class UserCreateValidator : AbstractValidator<UserCreateCommand>
//    {
//      public UserCreateValidator()
//      {
//        RuleFor(x => x.Id).NotEmpty().NotNull();
//        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
//        RuleFor(x => x.PasswordHash).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
//        RuleFor(x => x.PasswordSalt).NotEmpty().NotNull().WithMessage("Error, el producto necesita una categoría");
//      }
//    }

//    //public class UserCreateHandler : IUserRepositorio
//    //{
//    //  private readonly ApplicationDbContext _dbProduct;
//    //  private readonly IConfiguration _configuration;

//    //  public UserCreateHandler(ApplicationDbContext dbProduct, IConfiguration configuration)
//    //  {
//    //    _dbProduct = dbProduct;
//    //    _configuration = configuration;
//    //  }

//    public class UserCreateHandler : IRequestHandler<UserCreateCommand, string>
//    {
//      private readonly IUserRepositorio<User> dbProduct;
//      private readonly IConfiguration _configuration;


//      public UserCreateHandler(IUserRepositorio<User> dbProduct, ApplicationDbContext _dbProduct)
//      {
//        this.dbProduct = dbProduct;
//      }

//      public async Task<string> Login(string userName, string password)
//      {
//        var user = await dbProduct.Users.FirstOrDefaultAsync(
//            x => x.UserName.ToLower().Equals(userName.ToLower()));

//        if (user == null)
//        {
//          return "nouser";
//        }
//        else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//        {
//          return "wrongpassword";
//        }
//        else
//        {
//          return CrearToken(user);
//        }

//      }

//      public async Task<string> Register(User user, string password)
//      {
//        try
//        {

//          if (await UserExiste(user.UserName))
//          {
//            return "existe";
//          }

//          CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

//          user.PasswordHash = passwordHash;
//          user.PasswordSalt = passwordSalt;

//          await dbProduct.Users.AddAsync(user);
//          await dbProduct.SaveChangesAsync();
//          return CrearToken(user);
//        }
//        catch (Exception)
//        {

//          return "error";
//        }
//      }

//      public async Task<bool> UserExiste(string username)
//      {
//        if (await dbProduct.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
//        {
//          return true;
//        }
//        return false;
//      }


//      private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//      {
//        using (var hmac = new System.Security.Cryptography.HMACSHA512())
//        {
//          passwordSalt = hmac.Key;
//          passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//        }
//      }


//      public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
//      {
//        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//        {
//          var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//          for (int i = 0; i < computedHash.Length; i++)
//          {
//            if (computedHash[i] != passwordHash[i])
//            {
//              return false;
//            }
//          }
//          return true;
//        }
//      }


//      private string CrearToken(User user)
//      {
//        var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.UserName)
//            };

//        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
//                                    GetBytes(_configuration.GetSection("AppSettings:Token").Value));

//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//          Subject = new ClaimsIdentity(claims),
//          Expires = System.DateTime.Now.AddDays(1),
//          SigningCredentials = creds
//        };

//        var tokenHandler = new JwtSecurityTokenHandler();
//        var token = tokenHandler.CreateToken(tokenDescriptor);

//        return tokenHandler.WriteToken(token);

//      }

//      public Task<string> Handle(UserCreateCommand request, CancellationToken cancellationToken)
//      {
//        throw new NotImplementedException();
//      }
//    }
//  }
//}
