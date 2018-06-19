using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManagementStudent_01.ViewModels
{
    public class UpdateStudentModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int ClassId { get; set; }
    }
}