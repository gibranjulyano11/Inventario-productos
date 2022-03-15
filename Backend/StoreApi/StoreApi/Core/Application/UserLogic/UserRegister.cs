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
//using StoreApi.Core.Modelos;
//using StoreApi.Core.Repositorio;

//namespace StoreApi.Core.Application.ProductLogic
//{
//    public class UserRegister
//    {
//        public class UserRegisterCommand : IRequest<string>
//        {
//            public string UserName { get; set; }

//            public string Password { get; set; }

//        }
//        public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
//        {
//            public UserRegisterValidator()
//            {
//                RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
//                RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
//            }
//        }


//        public class UserRegisterHandler : IUserRepositorio
//        {
//            private readonly ApplicationDbContext _dbProduct;
//            private readonly IConfiguration _configuration;


//            public UserRegisterHandler(ApplicationDbContext dbProduct, IConfiguration configuration)
//            {
//                _dbProduct = dbProduct;
//                _configuration = configuration;
//            }

//            public async Task<string> Login(string userName, string password)
//            {
//                var user = await _dbProduct.User.FirstOrDefaultAsync(
//                    x => x.UserName.ToLower().Equals(userName.ToLower()));

//                if (user == null)
//                {
//                    return "nouser";
//                }
//                else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//                {
//                    return "wrongpassword";
//                }
//                else
//                {
//                    return CrearToken(user);
//                }

//            }

//            public async Task<string> Register(Users user, string password)
//            {
//                try
//                {

//                    if (await UserExiste(user.UserName))
//                    {
//                        return "existe";
//                    }

//                    CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

//                    user.PasswordHash = passwordHash;
//                    user.PasswordSalt = passwordSalt;

//                    await _dbProduct.User.AddAsync(user);
//                    await _dbProduct.SaveChangesAsync();
//                    return CrearToken(user);
//                }
//                catch (Exception)
//                {

//                    return "error";
//                }
//            }

//            public async Task<bool> UserExiste(string username)
//            {
//                if (await _dbProduct.User.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
//                {
//                    return true;
//                }
//                return false;
//            }


//            private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//            {
//                using (var hmac = new System.Security.Cryptography.HMACSHA512())
//                {
//                    passwordSalt = hmac.Key;
//                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//                }
//            }


//            public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
//            {
//                using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//                {
//                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//                    for (int i = 0; i < computedHash.Length; i++)
//                    {
//                        if (computedHash[i] != passwordHash[i])
//                        {
//                            return false;
//                        }
//                    }
//                    return true;
//                }
//            }
//            public Task<string> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
//            {
//                throw new NotImplementedException();
//            }

//            //public Task<string> Register(Users user, string password)
//            //{
//            //    throw new NotImplementedException();
//            //}
//            private string CrearToken(Users user)
//            {
//                var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.UserName)
//            };

//                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
//                                            GetBytes(_configuration.GetSection("Secret").Value));

//                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//                var tokenDescriptor = new SecurityTokenDescriptor
//                {
//                    Subject = new ClaimsIdentity(claims),
//                    Expires = System.DateTime.Now.AddDays(1),
//                    SigningCredentials = creds
//                };

//                var tokenHandler = new JwtSecurityTokenHandler();
//                var token = tokenHandler.CreateToken(tokenDescriptor);

//                return tokenHandler.WriteToken(token);

//            }
//        }
//    }
//}

////public class UserRegisterHandler : IUserRepositorio
////{
////  private readonly ApplicationDbContext _dbProduct;
////  private readonly IConfiguration _configuration;

////  public UserCreateHandler(ApplicationDbContext dbProduct, IConfiguration configuration)
////  {
////    _dbProduct = dbProduct;
////    _configuration = configuration;
////  }