using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class JOURNAL
    {
         [Key]
        public int JRVID { get; set; }
        public int MonthlyJVNO { get; set; }
        public string VoucherTYPE { get; set; }
        public int VoucherTypeId { get; set; }
        public DateTime JRTRANDATE { get; set; }
        public DateTime JRENTDATE { get; set; }
        public decimal PostedUser { get; set; }
        public bool PostedStatus { get; set; }
        public string VoucherNo { get; set; }
        public int FinancialYaerID { get; set; }
        public int formid { get; set; }
        public int CurrencyID { get; set; }
        public decimal ExchangeRate { get; set; }
        public int PaymentModeID { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public bool IsTransactionVoucher { get; set; }
        public string Payee { get; set; }
        public int AccountId { get; set; }
        public int CheuquePrintId { get; set; }
        public string UserID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }

        //  public JOURNAL Purchase { get; set; }
        public List<JOURNALDT> DetailItems { get; set; }

    }
}
