using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Model.ViewModels
{
  public  class ChartofAccountViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Account Code must be less or equal to 15 char.")]
        public string Code { get; set; }
        [MaxLength(150, ErrorMessage = "Account Name must be less or equal to 150 char.")]
        public string Name { get; set; }
        public short AccountLevel { get; set; }
        public int ParentId { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string NoteNo { get; set; }
        public string NoteDescription { get; set; }
        public int BranchId { get; set; }
        public string Active { get; set; }
        public string RequireCostCenter { get; set; }
        public string RequireSubAccount { get; set; }
        public int NewCodeLength { get; set; }
        public string[] SubAccountId { get; set; }
        public IEnumerable<GLSAccount> SubAccountList { get; set; }
        public object SubAccount { get; set; }
        public string Action { get; set; }
    }
}
