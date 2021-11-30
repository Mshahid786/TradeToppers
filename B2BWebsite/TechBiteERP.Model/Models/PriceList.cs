using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
    public class PriceList
    {
        [Key]
        public int Id { get; set; }
        public int ItemID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fromdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime Todate { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public int BrandId { get; set; }
        public int PriceCategoryId { get; set; }
        public int PackingId { get; set; }
        public decimal Price { get; set; }
        public decimal TagPrice { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public  Item Item { get; set; }
    }
}
