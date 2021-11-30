using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class Item
    {
        [Key]
        public int ItemID { get; set; }
        public int StockGLAccountId { get; set; }
        public int MaterialGLAccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string PackigSize { get; set; }
        public string PurchasePrice { get; set; }
        public int ItemL1 { get; set; }
        public int Iteml2 { get; set; }
        public int Iteml3 { get; set; }
        public int Iteml4 { get; set; }
        public int Iteml5 { get; set; }
        public int Iteml6 { get; set; }
        public int ItemP1 { get; set; }
        public int ItemP2 { get; set; }
        public int ItemP3 { get; set; }
        public int ItemP4 { get; set; }
        public int ItemP5 { get; set; }
        public int ItemP6 { get; set; }
        public string ItemHead { get; set; }
        public int ItemLevel { get; set; }
        public int ItemParent { get; set; }
        public bool ItemStatus { get; set; }
        public bool ItemClassID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal budgetedcost { get; set; }
        public bool isWaranty { get; set; }
        public string CompanyWaranty { get; set; }
        public string SupplierWaranty { get; set; }
        public int MasterItem { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public string Barcode3 { get; set; }
        public string Barcode4 { get; set; }
        public string Barcode5 { get; set; }
        public string Barcode6 { get; set; }
        public string Barcode7 { get; set; }
        public string Barcode8 { get; set; }
        public string Barcode9 { get; set; }
        public string Barcode10 { get; set; }
        public string itemcatalogno { get; set; }
        public int UnitID { get; set; }
        public int PackingTypeid { get; set; }
        public string PackedQuantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PackedUnits { get; set; }
        public int CartonSize { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemMinLevel { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemMaxLevel { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemReOrderlevel { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemReOrderQty { get; set; }
        public int Origionid { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeadTime { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeastTime { get; set; }
        public bool Stopautoreq { get; set; }
        public int ColorID { get; set; }
        public int MeterialID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        public int ItemCategoryID { get; set; }
        public string PriceCategoryID { get; set; }
        public string ItemRefNo { get; set; }
        public bool IsBatchedItem { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemLength { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemWidth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemHeight { get; set; }
        public int DefaultSupplierID { get; set; }
        public string Picture1Name { get; set; }
        public string Picture2Name { get; set; }
        public string Picture3Name { get; set; }
        public string Picture4Name { get; set; }
        public string Picture5Name { get; set; }
        public string Picture6Name { get; set; }
        public string VideoName { get; set; }
        public string ItemShadeCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemStrength { get; set; }
        public string GenericCode { get; set; }
        public int BrandID { get; set; }
        public int ItemTypeID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ManufacturingUnit { get; set; }
        public int ItemGroupCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesTaxPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ManufacturerRetailPirce { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TradePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RetailPrice { get; set; }
        public string BlockID { get; set; }
        public int Block { get; set; }

        public string SizeID { get; set; }
        public string LocationNo { get; set; }
        public int Companyid { get; set; }
        public int Branchid { get; set; }
        public string Remarks { get; set; }
        public string BinNo { get; set; }
    }
}
