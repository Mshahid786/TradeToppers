using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class JOURNALDT
    {
        [Key]
        public int JournalDetID { get; set; }
        public int Jrvid { get; set; }
        public int JOURNALJRVID { get; set; }
        public int ACNO { get; set; }
        public string ACFULLNO { get; set; }
        public string ACNAME { get; set; }
        public DateTime JRENTDATE { get; set; }
        public string JRPARTICULAR { get; set; }
        public decimal JRDR { get; set; }
        public decimal JRCR { get; set; }
        public decimal JRCFNO { get; set; }
        public int ItemID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
   
    }
}
