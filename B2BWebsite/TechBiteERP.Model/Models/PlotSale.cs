using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
    public class PlotSale
    {
        [Key]
        public int Id { get; set; }
        public int PlotId { get; set; }
        public string PlotName { get; set; }
        public string Block { get; set; }
        public string Size { get; set; }
        public int PlotStatusId { get; set; }
        public int Factor { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Height { get; set; }
        public decimal Price { get; set; }
        public string ReservedBy { get; set; }
        public string ReservedFor { get; set; }
        public int ReservedForMMD { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public string CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public bool Canceled { get; set; }

        public PlotStatus PlotStatus { get; set; }
        public Item Plot { get; set; }

    }
}
