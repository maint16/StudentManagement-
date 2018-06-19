using System.ComponentModel.DataAnnotations;
using ApiMultiPartFormData.Models;
using ManagementStudent_01.ViewModels.Attachment;

namespace ManagementStudent_01.ViewModels
{
    public class AddStudentViewModel
    { 
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public int ClassId { get; set; }
        public HttpFile Attachment { get; set; }
    }
}