using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class PaymentHistoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentHistoryController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.PaymentHistory.Include(x=>x.Plot).ToList();
             
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
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.ItemClass = new SelectList(_db.ItemClasses.ToList(), "ID", "Name");
            ViewBag.PayType = new SelectList(_db.PayType.ToList(), "ID", "Name");
            ViewBag.PaymentType = new SelectList(_db.PaymentType.ToList(), "ID", "Name");
            
            if (UserName != "" && UserName != null)
            {
                ViewBag.DeliveredBy = new SelectList(_db.Users.Where(x => x.UserName == UserName).ToList(), "Id", "UserName");
            }
            else
            {
                ViewBag.DeliveredBy = null;
            }
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            ViewBag.Customers = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType!=3).ToList(), "Id", "Name");
            PaymentHistory model = new PaymentHistory();
            if (id == 0)
            {
                model = new PaymentHistory();
                model.Date = DateTime.Now;
                 
            }
            else
            {
                model = _db.PaymentHistory.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PaymentHistory model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var BranchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            if (model.Id == 0)
            {
                
                model.CreationDate = DateTime.Now;
                model.CompanyId = companyId;
                model.BranchId = BranchId;
                model.CreatedUser = currentUser;
                model.FinancialYearId = FinancialYearId;
                _db.PaymentHistory.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PaymentHistory.Find(model.Id);
                model.CreationDate = obj.CreationDate;
                model.CreatedUser = obj.CreatedUser;
                obj = model;
                obj.UpdationDate = DateTime.Now;
                obj.CompanyId = companyId;
                obj.BranchId = BranchId;
                obj.UpdatedUser = currentUser;
                _db.PaymentHistory.Update(obj);
                await _db.SaveChangesAsync();

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PaymentHistory");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PaymentHistory.Find(id);
            var ent = _db.PaymentHistory.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult getRegisterationDetail(int PlotId,int blockId)
        //{
        //    var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId && x.BlockId==blockId).FirstOrDefault();
        //    return Ok(item);

        //}
        public IActionResult getRegisterationDetail(int PlotId)
        {
            var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId ).FirstOrDefault();
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
