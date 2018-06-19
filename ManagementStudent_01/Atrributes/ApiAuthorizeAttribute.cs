using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using JWT;
using JWT.Serializers;
using ManagementStudent_01.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ManagementStudent_01.Atrributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public readonly StudentManagementEntities DbSet;

        public ApiAuthorizeAttribute()
        {
            DbSet = new StudentManagementEntities();
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization.Parameter;
            const string secret = "gjhgjhgmjgjmhgjhtjmjmgjmgjmgjhm";

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                var m = JsonConvert.DeserializeObject<UserModel>(json);
                var user = DbSet.Users.FirstOrDefault(c => c.Id == m.Id);
                actionContext.ActionArguments["User"] = user;
                if (user != null)
                    return;

                
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
        }
    }
}