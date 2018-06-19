using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManagementStudent_01.ViewModels
{
    public class TokenModel
    {
        public string AccessToken { get; set; }

        public string Type { get; set; }

        public int LifeTime { get; set; }
    }
}