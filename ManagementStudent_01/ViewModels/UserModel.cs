using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManagementStudent_01.ViewModels
{
    public class UserModel
    {
       

        public int Id { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
    }
}