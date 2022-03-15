//namespace StoreApi.Core.Application.UserLogic
//{
//    public class UserLogin
//    {
//        public class UserRegister
//        {
//            public class UserRegisterCommand : IRequest<string>
//            {
//                public string UserName { get; set; }

//                public string Password { get; set; }

//            }
//            public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
//            {
//                public UserRegisterValidator()
//                {
//                    RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
//                    RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
//                }
//            }


//            public class UserRegisterHandler : IUserRepositorio
//            {
//                private readonly ApplicationDbContext _dbProduct;
//                private readonly IConfiguration _configuration;


//                public UserRegisterHandler(ApplicationDbContext dbProduct, IConfiguration configuration)
//                {
//                    _dbProduct = dbProduct;
//                    _configuration = configuration;
//                }

//                public async Task<string> Login(string userName, string password)
//                {
//                    var user = await _dbProduct.User.FirstOrDefaultAsync(
//                        x => x.UserName.ToLower().Equals(userName.ToLower()));

//                    if (user == null)
//                    {
//                        return "nouser";
//                    }
//                    else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//                    {
//                        return "wrongpassword";
//                    }
//                    else
//                    {
//                        return CrearToken(user);
//                    }

//                }
//            }
//}
