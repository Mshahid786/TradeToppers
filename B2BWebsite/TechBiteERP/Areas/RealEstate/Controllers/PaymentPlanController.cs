using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Authorize]
    public class PaymentPlanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentPlanController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.PaymentPlan.ToList();
             
            return View(list);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.CompanyName = HttpContext.Session.GetString("CompanyName");
            ViewBag.BranchId = HttpContext.Session.GetInt32("BranchId");
            var UserId = HttpContext.Session.GetString("UserId");
            var UserName = HttpContext.Session.GetString("UserName");

            ViewBag.Colors = new SelectList(_db.Colors.ToList(), "ID", "Name");
            // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            // ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.ItemClass = new SelectList(_db.ItemClasses.ToList(), "ID", "Name");
            ViewBag.PayType = new SelectList(_db.PayType.ToList(), "ID", "Name");
            ViewBag.PaymentType = new SelectList(_db.PaymentType.ToList(), "ID", "Name");
            if (UserName != "" && UserName == null)
            {
                ViewBag.DeliveredBy = new SelectList(_db.Users.Where(x => x.UserName == UserName).ToList(), "Id", "UserName");
            }
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            ViewBag.CustomerMMD = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType==3).ToList(), "Id", "Name");
            PaymentPlan model = new PaymentPlan();
            if (id == 0)
            {
                model = new PaymentPlan();
                model.BookingDate = DateTime.Now;   
            }
            else
            {
                model = _db.PaymentPlan.Find(id);
                var items = _db.PaymentPlanDetails.Where(i => i.PaymentPlanId == id).ToList();
                model.PaymentPlanDetails = items;  
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PaymentPlan model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var BranchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            PaymentPlanDetail[] modelItems = JsonConvert.DeserializeObject<PaymentPlanDetail[]>(collection["tblPaymentPlanDetails"]);
            if (model.Id == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = companyId;
                model.BranchId = BranchId;
                model.FinancialYearId = FinancialYearId;
                model.CreatedUser = currentUser;
                _db.PaymentPlan.Add(model);
                await _db.SaveChangesAsync();

                foreach (var item in modelItems)
                {
                    PaymentPlanDetail detail = new PaymentPlanDetail();
                    detail.PaymentPlanId = model.Id;
                    detail.Sr = item.Sr;
                    detail.DueDate = item.DueDate;
                    detail.Amount = item.Amount;
                    detail.BubbleDuration = item.BubbleDuration;
                    _db.PaymentPlanDetails.Add(detail);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var obj = _db.PaymentPlan.Find(model.Id);
                obj = model;
                //model.CompanyId = companyId;
                //model.BranchId = BranchId;
                //model.FinancialYearId = FinancialYearId;
                //model.CreatedUser = currentUser;
                _db.PaymentPlan.Update(obj);
                await _db.SaveChangesAsync();

                //Delete Previous entry
                var entry = _db.PaymentPlanDetails.Where(x => x.PaymentPlanId == obj.Id);
                foreach (var dt in entry)
                {
                     _db.PaymentPlanDetails.Remove(dt);
                   // _db.PaymentPlanDetails.Remove(entrydelete);
                    
                }
                _db.SaveChanges();
                //Insert Detail
                foreach (var item in modelItems)
                {
                   

                    PaymentPlanDetail detail = new PaymentPlanDetail();
                    //var detail = _db.PaymentPlanDetails.Find(item.PlanDetailId);
                    //if (detail != null)
                    //{
                    //    detail.PaymentPlanId = model.Id;
                    //    detail.Sr = item.Sr;
                    //    detail.DueDate = item.DueDate;
                    //    detail.Amount = item.Amount;

                    //    _db.PaymentPlanDetails.Update(detail);
                    //    await _db.SaveChangesAsync();
                    //}
                    //else
                    //{

                    detail = new PaymentPlanDetail();
                        detail.PaymentPlanId = model.Id;
                        detail.Sr = item.Sr;
                        detail.DueDate = item.DueDate;
                        detail.Amount = item.Amount;
                        _db.PaymentPlanDetails.Add(detail);
                        await _db.SaveChangesAsync();

                    //}

                }

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PaymentPlan");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PaymentPlan.Find(id);
            var ent = _db.PaymentPlan.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult getRegisterationDetail(int PlotId,int blockId)
        {
            var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId && x.BlockId==blockId).FirstOrDefault();
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
