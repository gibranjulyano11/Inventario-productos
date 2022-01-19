using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StoreApi.Core.Domain;
using StoreApi.Helpers;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly IConfiguration conf;

        public UsersController(IConfiguration config)
        {
            conf = config;
        }


        [HttpGet]
        public IEnumerable<object> GetAll()
        {
            var usuarios = new List<object>(){
                new {
                    nombre = "Crico Mosquera",
                    rol = "Admin"
                },
                new {
                    nombre = "Ashor Nsr",
                    rol = "Empleado"
                },
            };

            return usuarios;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<object> Login([FromBody] User user)
        {
            string secret = this.conf.GetValue<string>("Secret");
            var jwtHelper = new JWTHelper(secret);
            var token = jwtHelper.CreateToken(user.UserName);

            return Ok(new
            {
                ok = true,
                msg = "Has iniciado sesión exitosamente",
                token
            });
        }
    }
}
