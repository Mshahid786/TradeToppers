using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _db;

        public JournalController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.ToList(), "VoucherTypesID", "Type");
            var model = _db.JOURNAL.ToList();
            return View(model);
        }

        public int GetMaxNo(int vtype)
        {
            int maxNo = 1;
            var entry = _db.JOURNAL.Where(x=>x.VoucherTypeId==vtype).ToList();
            if (entry.Count > 0)
            {
                maxNo = entry.Max(x => Convert.ToInt32( x.MonthlyJVNO)) + 1;
            }
            return maxNo;
        }
        [HttpGet]

        public IActionResult Create(int id , string type)
        {
             
            var vtype =  (_db.VoucherTypes.Where(x => x.Type == type)).FirstOrDefault();
            
            ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.Where(x=>x.Type==type).ToList(), "VoucherTypesID", "VoucherTypeDescription");
            ViewBag.Accounts = new SelectList((from s in _db.GLAccounts.Where(x => x.IsDeleted == false && x.IsActive == true && x.CompanyId == 1)
                                               select new
                                               {
                                                   Id = s.Id,
                                                   Name = s.Code + " - " + s.Name
                                               }).ToList(), "Id", "Name");
            ViewBag.Heading =Convert.ToString( vtype.VoucherTypeDescription);
            JOURNAL model = new JOURNAL();
            if (id == 0)
            {
                var maxNo = GetMaxNo(vtype.VoucherTypesID);
                string VoucherNo = type + "-" + maxNo + "-" + DateTime.Today.Date.ToString("yy-MM-dd");
                TempData["No"] = maxNo;
                model = new JOURNAL();
                model.VoucherNo = VoucherNo;
                model.JRTRANDATE = DateTime.Now;
                model.MonthlyJVNO = maxNo;
                model.VoucherTYPE =( from t in _db.VoucherTypes.Where(x=>x.Type==type) select t.Type).FirstOrDefault();
            }
            else
            {
                
                model = _db.JOURNAL.Find(id);
                ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.Where(x => x.Type == model.VoucherTYPE).ToList(), "VoucherTypesID", "VoucherTypeDescription");
                var items = _db.JOURNALDT.Where(i => i.JOURNALJRVID == id ).ToList();
                model.DetailItems = items;
                ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.Where(x => x.VoucherTypesID == model.VoucherTypeId).ToList(), "VoucherTypesID", "VoucherTypeDescription");
                TempData["No"] = model.MonthlyJVNO;
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(JOURNAL model, IFormCollection collection)
        {
           // var modelItems = collection["tblvoucherDetails"];
            JOURNALDT[] modelItems = JsonConvert.DeserializeObject<JOURNALDT[]>(collection["tblvoucherDetails"]);
            if (modelItems != null)
            {
                if (model.JRVID == 0)
                {
                    model.CompanyID = 1;
                    model.BranchID = 1;
                    model.MonthlyJVNO = GetMaxNo(model.VoucherTypeId);
                  var entry =  _db.JOURNAL.Add(model);
                    await _db.SaveChangesAsync();

                   foreach(var item in modelItems)
                    {
                        JOURNALDT detail = new JOURNALDT();
                        detail.Jrvid = model.JRVID;
                        detail.JOURNALJRVID = model.JRVID;
                        detail.ACNO = item.ACNO;
                        detail.ACNAME = item.ACNAME;
                        detail.ItemID = item.ItemID;
                        detail.JRDR = item.JRDR;
                        detail.JRCR = item.JRCR;
                        detail.JRPARTICULAR = item.JRPARTICULAR;
                        detail.JRCFNO = item.JRCFNO;
                        detail.JRPARTICULAR = item.JRPARTICULAR;

                        _db.JOURNALDT.Add(detail);
                        await _db.SaveChangesAsync();
                    }

                }
                else
                {

                    var obj = _db.JOURNAL.Find(model.JRVID);
                    // obj = model;
                    obj.VoucherTYPE = model.VoucherTYPE;
                    obj.JRTRANDATE = model.JRTRANDATE;
                    obj.VoucherTypeId = model.VoucherTypeId;
                    obj.CheuquePrintId = model.CheuquePrintId;
                    obj.Payee = model.Payee;
                    obj.PaymentModeID = model.PaymentModeID;
                    obj.IsTransactionVoucher = model.IsTransactionVoucher;
                    obj.PostedStatus = model.PostedStatus;
                    obj.PostedUser = model.PostedUser;
                    obj.TotalDebit = model.TotalDebit;
                    obj.TotalCredit = model.TotalCredit;
                    _db.JOURNAL.Update(obj);

                    await _db.SaveChangesAsync();

                    foreach (var item in modelItems)
                    {
                        var detail = _db.JOURNALDT.Find(item.JournalDetID);
                        if (detail != null)
                        {
                            detail.JOURNALJRVID = model.JRVID;
                            detail.ACNO = item.ACNO;
                            detail.ACNAME = item.ACNAME;
                            detail.ItemID = item.ItemID;
                            detail.JRDR = item.JRDR;
                            detail.JRCR = item.JRCR;
                            detail.JRPARTICULAR = item.JRPARTICULAR;
                            detail.JRCFNO = item.JRCFNO;
                            detail.JRPARTICULAR = item.JRPARTICULAR;

                            _db.JOURNALDT.Update(detail);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                              detail = new JOURNALDT();
                            detail.Jrvid = model.JRVID;
                            detail.JOURNALJRVID = model.JRVID;
                            detail.ACNO = item.ACNO;
                            detail.ACNAME = item.ACNAME;
                            detail.ItemID = item.ItemID;
                            detail.JRDR = item.JRDR;
                            detail.JRCR = item.JRCR;
                            detail.JRPARTICULAR = item.JRPARTICULAR;
                            detail.JRCFNO = item.JRCFNO;
                            detail.JRPARTICULAR = item.JRPARTICULAR;

                            _db.JOURNALDT.Add(detail);
                            await _db.SaveChangesAsync();

                        }

                    }
                }

            }
            else
            {
                TempData["ErrorMessage"] = "Enter AtLeast One Record in Detail";
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Create(APPurchaseItemViewModel vm, IFormCollection collection)
        //{
        //    int companyId = HttpContext.Session.GetInt32("CompanyId").Value;
        //    var userId = HttpContext.Session.GetString("UserId");
        //    var result = "";
        //    bool Ratezero = false;
        //    APPurchaseItem[] medicalsdata = JsonConvert.DeserializeObject<APPurchaseItem[]>(collection["details"]);
        //    foreach (var item in medicalsdata)
        //    {
        //        if (item.Rate <= 0)
        //        {
        //            Ratezero = true;
        //            break;
        //        }

        //    }

        //    try
        //    {
        //        if (vm.Id != 0)
        //        {
        //            APPurchase apPurFrm = _dbContext.APPurchases.Find(vm.Id);
        //            apPurFrm.Id = vm.Id;
        //            apPurFrm.BranchId = vm.BranchId;
        //            apPurFrm.PurchaseNo = vm.PurchaseNo;
        //            apPurFrm.PurchaseDate = vm.PurchaseDate;
        //            apPurFrm.ApprovedBy = userId;
        //            apPurFrm.Currency = vm.Currency;
        //            apPurFrm.CurrencyExchangeRate = vm.CurrencyExchangeRate;
        //            apPurFrm.GrandTotal = vm.GrandTotal;
        //            apPurFrm.IGPNo = vm.IGPNo;
        //            apPurFrm.SupplierInvoiceNo = vm.SupplierInvoiceNo;
        //            apPurFrm.SupplierInvoiceDate = vm.SupplierInvoiceDate;
        //            apPurFrm.PaymentTotal = vm.PaymentTotal;
        //            apPurFrm.PeriodId = vm.PeriodId;
        //            apPurFrm.Status = "Created";
        //            apPurFrm.Total = vm.Total;
        //            apPurFrm.TotalDiscountAmount = vm.TotalDiscountAmount;
        //            apPurFrm.TotalExciseTaxAmount = vm.TotalExciseTaxAmount;
        //            apPurFrm.TotalSalesTaxAmount = vm.TotalSalesTaxAmount;
        //            apPurFrm.TransactionType = "Purchase";
        //            apPurFrm.WareHouseId = vm.WareHouseId;
        //            apPurFrm.VoucherId = vm.VoucherId;
        //            apPurFrm.SalesPersonId = vm.SalesPersonId;
        //            apPurFrm.SupplierId = vm.SupplierId;
        //            apPurFrm.Remarks = vm.Remarks;
        //            apPurFrm.CompanyId = companyId;
        //            apPurFrm.IsDeleted = false;
        //            apPurFrm.UpdatedBy = userId;
        //            apPurFrm.UpdatedDate = DateTime.Now;
        //            apPurFrm.IsZero = Ratezero;
        //            _dbContext.APPurchases.Update(apPurFrm);
        //            _dbContext.SaveChanges();


        //            // Delete Items According to Master Id
        //            var PurchaseDelete = _dbContext.APPurchaseItems.Where(a => a.PurchaseId == vm.Id).ToList();
        //            if (PurchaseDelete.Count > 0)
        //            {
        //                foreach (var item in PurchaseDelete)
        //                {
        //                    _dbContext.APPurchaseItems.Remove(item);
        //                    _dbContext.SaveChanges();
        //                }
        //                foreach (var items in medicalsdata)
        //                {
        //                    APPurchaseItem data = new APPurchaseItem();
        //                    data = items;
        //                    data.PurchaseId = apPurFrm.Id;
        //                    data.Qty = items.SQM;
        //                    data.SQM = items.SQM;
        //                    data.Rate = items.Rate;
        //                    data.Total = items.Total;
        //                    data.CreatedBy = userId;
        //                    data.CreatedDate = DateTime.Now;
        //                    _dbContext.APPurchaseItems.Add(data);
        //                    _dbContext.SaveChanges();
        //                }


        //            }



        //            // return View(RedirectToAction(nameof(Index)));
        //            return RedirectToAction("Index", "Purchase");
        //        }
        //        else
        //        {

        //            APPurchase apPurFrom = new APPurchase();

        //            apPurFrom.Id = vm.Id;
        //            apPurFrom.BranchId = vm.BranchId;
        //            apPurFrom.PurchaseNo = GetMaxNo();
        //            apPurFrom.PurchaseDate = vm.PurchaseDate;
        //            apPurFrom.ApprovedBy = userId;
        //            apPurFrom.Currency = vm.Currency;
        //            apPurFrom.CurrencyExchangeRate = vm.CurrencyExchangeRate;
        //            apPurFrom.GrandTotal = vm.GrandTotal;
        //            apPurFrom.IGPNo = vm.IGPNo;
        //            apPurFrom.SupplierInvoiceNo = vm.SupplierInvoiceNo;
        //            apPurFrom.SupplierInvoiceDate = vm.SupplierInvoiceDate;
        //            apPurFrom.PaymentTotal = vm.PaymentTotal;
        //            apPurFrom.PeriodId = vm.PeriodId;
        //            apPurFrom.Status = "Created";
        //            apPurFrom.Total = vm.Total;
        //            apPurFrom.TotalDiscountAmount = vm.TotalDiscountAmount;
        //            apPurFrom.TotalExciseTaxAmount = vm.TotalExciseTaxAmount;
        //            apPurFrom.TotalSalesTaxAmount = vm.TotalSalesTaxAmount;
        //            apPurFrom.TransactionType = "Purchase";
        //            apPurFrom.WareHouseId = vm.WareHouseId;
        //            apPurFrom.VoucherId = vm.VoucherId;
        //            apPurFrom.SalesPersonId = vm.SalesPersonId;
        //            apPurFrom.SupplierId = vm.SupplierId;

        //            apPurFrom.Remarks = vm.Remarks;

        //            apPurFrom.CompanyId = companyId;
        //            apPurFrom.IsZero = Ratezero;
        //            apPurFrom.IsDeleted = false;
        //            apPurFrom.CreatedBy = userId;
        //            apPurFrom.CreatedDate = DateTime.Now;


        //            _dbContext.APPurchases.Add(apPurFrom);
        //            _dbContext.SaveChanges();

        //            APPurchaseItem[] purchaseDetail = JsonConvert.DeserializeObject<APPurchaseItem[]>(collection["details"]);

        //            foreach (APPurchaseItem item in purchaseDetail)
        //            {
        //                //HRDeductionEmployee experience = new HRDeductionEmployee();
        //                //experience.DeductionId = model.Id;
        //                //experience = employeeExperince;
        //                //experience.EmployeeId = Convert.ToInt32(collection["employeeId"]);
        //                //experience.CreatedBy = _userId;
        //                //experience.CreatedDate = DateTime.Now;
        //                //_dbContext.HRDeductionEmployees.Add(experience);
        //                //await _dbContext.SaveChangesAsync();

        //                APPurchaseItem detail = new APPurchaseItem();
        //                detail = item;
        //                detail.Id = 0;
        //                detail.PurchaseId = apPurFrom.Id;
        //                detail.Qty = item.SQM;
        //                detail.SQM = item.SQM;
        //                detail.Rate = item.Rate;
        //                detail.Total = item.Total;
        //                detail.CreatedBy = userId;
        //                detail.CreatedDate = DateTime.Now;


        //                _dbContext.APPurchaseItems.Add(detail);
        //                _dbContext.SaveChanges();
        //            }


        //            // return View(nameof(Index));
        //            return RedirectToAction("Create", new { id = apPurFrom.Id });
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        result = "Error";
        //    }

        //    return Ok(result);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.JOURNAL.Find(id);
            entry.PostedStatus = false;
            var ent = _db.JOURNAL.Update(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult GetUniversity(int id)
        {
            
            var universties = GetOrderViewItems(id);
            //List<ARSaleOrderViewModel> list = new List<ARSaleOrderViewModel>();
            //foreach (var item in universties)
            //{
            //    TempData["Customer"] = (from a in _dbContext.ARCustomers.Where(x => x.Id == item.CustomerId) select (a.Name ?? "")).FirstOrDefault();
            //    var itemName = (from i in _dbContext.InvItems.Where(x => x.Id == item.ItemId) select i.ItemFullName).FirstOrDefault();
            //    var model = new ARSaleOrderViewModel();
            //    model.ItemName = item.ItemName;
            //    model.Qty = item.Qty;
            //    model.Boxes = item.Boxes;
            //    model.Tiles = item.Tiles;
            //    model.Rate = item.Rate;
            //    model.Total = item.Total;
            //    list.Add(model);
            //}
            return PartialView("_partialJournalView", universties.ToList());
        }

        public List<JOURNALDT> GetOrderViewItems(int id)
        {
            var query = _db.JOURNAL.Join(_db.JOURNALDT, cd => cd.JRVID, cus => cus.JOURNALJRVID, (cd, cus) => new { JOURNAL = cd, JOURNALDT = cus })
                      .Join(_db.GLAccounts, x => x.JOURNALDT.ACNO, cr => cr.Id, (x, cr) => new { x.JOURNAL, x.JOURNALDT, Account = cr })
                      .Select(s => new JOURNALDT()
                      {
                          JournalDetID = s.JOURNALDT.JournalDetID,
                          JOURNALJRVID = s.JOURNAL.JRVID,
                          ACFULLNO = s.Account.Code,
                          ACNAME = s.JOURNALDT.ACNAME,
                          JRPARTICULAR = s.JOURNALDT.JRPARTICULAR,
                          JRDR = s.JOURNALDT.JRDR,
                          JRCR = s.JOURNALDT.JRCR
                      }).Where(x => x.JOURNALJRVID == id).ToList();
 
            return query;
        }


        public IActionResult Indexes(int? fromNo, int? toNo,int voucherTyeId,DateTime fromDate,DateTime toDate,string type,int IsPosted)
        {

            if (type == "ByDate")
            {
                if (voucherTyeId == 1) {
                    if (IsPosted == 2)
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x =>  x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                    else
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate && x.PostedStatus == Convert.ToBoolean(IsPosted)).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);

                    }
                }
                else
                {
                    if (IsPosted == 2)
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.VoucherTypeId == voucherTyeId && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                    else
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.VoucherTypeId == voucherTyeId && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate && x.PostedStatus == Convert.ToBoolean(IsPosted)).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);

                    }
                }

            }
            else
            {
                if (voucherTyeId == 1) {
                    if (IsPosted == 2)
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.MonthlyJVNO >= fromNo && x.MonthlyJVNO <= toNo && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                    else
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.MonthlyJVNO >= fromNo && x.MonthlyJVNO <= toNo && x.PostedStatus == Convert.ToBoolean(IsPosted) && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                }
                else
                {
                    if (IsPosted == 2)
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.VoucherTypeId == voucherTyeId && x.MonthlyJVNO >= fromNo && x.MonthlyJVNO <= toNo && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                    else
                    {
                        var order = (from o in _db.JOURNAL
                                     join br in _db.VoucherTypes on o.VoucherTypeId equals br.VoucherTypesID
                                     orderby o.MonthlyJVNO descending
                                     select new JOURNAL
                                     {
                                         JRVID = o.JRVID,
                                         VoucherNo = o.VoucherNo,
                                         MonthlyJVNO = o.MonthlyJVNO,
                                         JRENTDATE = o.JRENTDATE,
                                         JRTRANDATE = o.JRTRANDATE,
                                         VoucherTypeId = o.VoucherTypeId,
                                         VoucherTYPE = o.VoucherTYPE,
                                         TotalDebit = o.TotalDebit,
                                         TotalCredit = o.TotalDebit,
                                         PostedStatus = o.PostedStatus
                                     }).Where(x => x.VoucherTypeId == voucherTyeId && x.MonthlyJVNO >= fromNo && x.MonthlyJVNO <= toNo && x.PostedStatus == Convert.ToBoolean(IsPosted) && x.JRTRANDATE >= fromDate && x.JRTRANDATE <= toDate).OrderByDescending(x => x.VoucherNo).ToList();
                        return Json(order);
                    }
                }
            }

        }
        public IActionResult GetVoucherDetail(int? voucherId)
        {
            var order = (from m in _db.JOURNAL
                         join d in _db.JOURNALDT on m.JRVID equals d.JOURNALJRVID
                         orderby d.JournalDetID ascending
                         select new JOURNALDT
                         {
                             //JOURNALJRVID = s.JOURNAL.JRVID,
                             //ACFULLNO = s.Account.Code,
                             //ACNAME = s.JOURNALDT.ACNAME,
                             //JRPARTICULAR = s.JOURNALDT.JRPARTICULAR,
                             //JRDR = s.JOURNALDT.JRDR,
                             //JRCR = s.JOURNALDT.JRCR
                         }).ToList();
            return Json(order);
        }

        public IActionResult UpdateInvoiceStatus(string VoucherList, bool Status)
        {
            
                dynamic InvList = JsonConvert.DeserializeObject(VoucherList);
                foreach (var item in InvList)
                {
                    int id = item.voucher_id;
                    JOURNAL journal = _db.JOURNAL.Where(x => x.JRVID == id).FirstOrDefault();
                    journal.PostedStatus = Status;
                _db.JOURNAL.Update(journal);
                    _db.SaveChanges();
                }
                return Json("Success");   
        }
    }
}
