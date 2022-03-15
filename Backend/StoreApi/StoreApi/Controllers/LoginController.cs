//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using StoreApi.Core.Domain;
//using StoreApi.Helpers;
//using System.Collections.Generic;

//namespace StoreApi.Controllers
//{
//  [Route("api/login")]
//  [ApiController]
//  [Authorize]
//  public class LoginController : ControllerBase
//  {
//    private readonly IConfiguration conf;

//    public LoginController(IConfiguration config)
//    {
//      conf = config;
//    }


//    [HttpGet]
//    public IEnumerable<object> GetAll()
//    {
//      var users = new List<object>(){
//                    new {
//                        username = "capo2786",
//                        password = "Capo2786"
//                    },
//                };

//      return users;
//    }



//    [HttpPost]
//    [AllowAnonymous]
//    public ActionResult<object> Login([FromBody] User1 user)
//    {
//      string secret = this.conf.GetValue<string>("Secret");
//      var jwtHelper = new JWTHelper(secret);
//      var token = jwtHelper.CreateToken(user.UserName);

//      return Ok(new
//      {
//        ok = true,
//        msg = "Has iniciado sesión exitosamente",
//        token
//      });
//    }
//  }
//}
