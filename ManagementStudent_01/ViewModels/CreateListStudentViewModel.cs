using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManagementStudent_01.ViewModels
{
    public class CreateListStudentViewModel
    {  
        [Required]
        public List<AddStudentViewModel> Students { get; set; }
    }
}