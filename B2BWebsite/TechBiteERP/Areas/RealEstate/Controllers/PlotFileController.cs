using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PlotFileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotFileController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.PlotFile.ToList();
             
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
            ViewBag.PlotStatus = new SelectList(_db.PlotStatus.ToList(), "Id", "Name");
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            if (UserName != "" && UserName != null)
            {
                TempData["LoggedBy"] = _db.Users.Where(x => x.UserName == UserName).FirstOrDefault().UserName;
            }
            else
            {
                TempData["LoggedBy"] = "";
            }
            
            ViewBag.DeliveredVia = new SelectList(_db.DeliveredVia.ToList(), "ID", "Name");
            ViewBag.DeliveredBy = new SelectList(_db.ArCustomer.Where(x => x.CustomerType == 3).ToList(), "Id", "Name");
            ViewBag.CustomerMMD = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType==3).ToList(), "Id", "Name");
            PlotFile model = new PlotFile();
            if (id == 0)
            {

                model = new PlotFile();
                model.LogDate = DateTime.Now;
            }
            else
            {
                model = _db.PlotFile.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotFile model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            if (model.Id == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = companyId;
                model.BranchId = branchId;
                model.CreatedUser = currentUser;
                model.FinancialYearId = FinancialYearId;
                _db.PlotFile.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PlotFile.Find(model.Id);
                obj.PlotId = model.PlotId;
                obj.DeliveredTo = model.DeliveredTo;
                obj.DeliveredVia = model.DeliveredVia;
                obj.CourierName = model.CourierName;
                obj.TrackingNo = model.TrackingNo;
                obj.DeliveredBy = model.DeliveredBy;
                obj.LoggedBy = model.LoggedBy;
                obj.LogDate = model.LogDate;
                obj.CompanyId = model.CompanyId;
                obj.BranchId = model.BranchId;
                obj.Block = model.Block;
                obj.FinancialYearId = model.FinancialYearId;
                obj.UpdationDate = DateTime.Now;
                obj.UpdatedUser = currentUser;
                _db.PlotFile.Update(obj);
                await _db.SaveChangesAsync();

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PlotFile");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotFile.Find(id);
            var ent = _db.PlotFile.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult getItemDetail(int id)
        {
            var item = _db.Items.Find(id);
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
