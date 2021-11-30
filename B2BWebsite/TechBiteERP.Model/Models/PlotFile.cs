using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PlotFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Block { get; set; }
        [Required]
        public int PlotId { get; set; }
        [Required]
        public string DeliveredTo { get; set; }
        public int DeliveredVia { get; set; }
        [Required]
        public string CourierName { get; set; }
        public string TrackingNo { get; set; }
        [Required]
        public int DeliveredBy { get; set; }
        public string LoggedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime LogDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public bool Deleted { get; set; }

    }
}
