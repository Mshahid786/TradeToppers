using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class PlotStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
