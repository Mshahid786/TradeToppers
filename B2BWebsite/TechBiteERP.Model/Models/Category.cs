using System;
using System.Collections.Generic;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Category
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
