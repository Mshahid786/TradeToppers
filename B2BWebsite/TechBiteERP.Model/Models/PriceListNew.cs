using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PriceListNew
    {
        [Key]
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public bool IsDefault { get; set; }

        public List<PriceListNewDetail> PriceListDetails { get; set; }

    }
}
