using JWT.Algorithms;
using ManagementStudent_01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWT;
using JWT.Serializers;

namespace ManagementStudent_01.Controllers
{
    [RoutePrefix("api/user")]
    public class ApiUserController : ApiController
    {
        public readonly StudentManagementEntities DbSet;

        public ApiUserController()
        {
            DbSet=new StudentManagementEntities();
        }
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginViewModel model)
        {
            if (model == null)
            {
                model=new LoginViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = DbSet.Users.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);
            if (user != null)
            {
                
                const string secret = "gjhgjhgmjgjmhgjhtjmjmgjmgjmgjhm";

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                var payload = new Dictionary<string, object>
                {
                    { "Id", user.Id }

                };
                var token = encoder.Encode(payload, secret);
               
                var result = new TokenModel()
                {
                    AccessToken = token,
                    Type = "Bearer",
                    LifeTime = 0
                };

                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
