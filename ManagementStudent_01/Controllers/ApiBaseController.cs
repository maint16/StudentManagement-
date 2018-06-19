using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagementStudent_01.Atrributes;

namespace ManagementStudent_01.Controllers
{
    [ApiAuthorize]
    public class ApiBaseController : ApiController
    {
    }
}
