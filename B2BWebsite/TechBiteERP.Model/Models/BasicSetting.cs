using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class BasicSetting
    {
        [Key]
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int CashAccountId { get; set; }
        public int RFAccountId { get; set; }
        public int unappPlsDetailAccountId { get; set; }
        public int CashSaleAccountId { get; set; }
        public int SalesDiscountAccountId { get; set; }
        public int FreightInwardAccontId { get; set; }
        public int MiscExpensesAccountId { get; set; }
        public int AdjSalesRAccountId { get; set; }
        public int SaleTaxPayableAccountId { get; set; }
        public int SaleTaxRefundableAccontId { get; set; }
        public int MarketingExpenseAccountId { get; set; }
        public int WastageAccountId { get; set; }
        public int CollecctionDiscontId { get; set; }
        public int AnnualDiscountId { get; set; }
        public int TokenAccountId { get; set; }
        public int BiltyAccountId { get; set; }
        public int OtherAccountId { get; set; }
        public int AdjustedItemAccountId { get; set; }
        public int SamplefocAccountId { get; set; }
        public int PurchaseVoucherType { get; set; }
        public int SalesVoucherType { get; set; }
        public int DebitNoteVoucher { get; set; }
        public int CreditNoteVoucher { get; set; }
        public int SalesRetrnVType { get; set; }
        public int PurchaseReturnVType { get; set; }
        public int MaterialIssueVType { get; set; }
        public int IssueReturnVType { get; set; }
        public int FGQAVType { get; set; }
        public int SampleIssueVType { get; set; }
        public int StockAdjVType { get; set; }
        public int STNVoucherType { get; set; }
        public int STRVoucherType { get; set; }
        public int CreditorsControl { get; set; }
        public int DebitorsControl { get; set; }
        public int WHTPayable { get; set; }
        public string WarrentyText { get; set; }
 
        
    }
}
