using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStudent_01.ViewModels.Attachment
{
    public class CreateAttachmentViewModel:BaseViewModel
    {
        public  string FileName { get; set; }
        public  string Mime { get; set; }
        public  byte[] File { get; set; }
        public string Extension { get; set; }
    }
}