using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using JWT;
using JWT.Serializers;
using ManagementStudent_01.ViewModels;
using Newtonsoft.Json;

namespace ManagementStudent_01.Atrributes
{
    public class ApiRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;
        public readonly StudentManagementEntities DbSet;
        public ApiRoleAttribute(string[] roles)
        {
            _roles = roles;
            DbSet = new StudentManagementEntities();
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var a = actionContext.ActionArguments["User"];
            var user = a as User;
            try
            {
                if (user != null && _roles.Contains(user.Role))
                    return;
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
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