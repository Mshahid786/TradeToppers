using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechBiteERP.Model.Models
{
    public class FileUploadViewModel
    {
        public IFormFile XlsFile { get; set; }
        /*create StaffInfoViewModel  object because we need to add read
         excel data and mapping in StaffInfoViewModel*/
        public StaffInfoViewModel StaffInfoViewModel { get; set; }
        public FileUploadViewModel()//Create contractor
        {
            //call StaffInfoViewModel  this object in contractor
            StaffInfoViewModel = new StaffInfoViewModel();
        }
    }
}
