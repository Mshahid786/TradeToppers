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
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SaleController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
            
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var list = _db.PlotSale.Where(x=>x.BranchId==branchId).Include(x=>x.PlotStatus).ToList();
            foreach(var item in list)
            {
                item.PlotName = (from a in _db.Items.Where(x => x.ItemID == item.PlotId) select a.Description).FirstOrDefault();
            }
            return View(list);
        }

    
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Colors = new SelectList(_db.Colors.ToList(), "ID", "Name");
            // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.ItemClass = new SelectList(_db.ItemClasses.ToList(), "ID", "Name");
            ViewBag.PlotStatus = new SelectList(_db.PlotStatus.ToList(), "Id", "Name");
            ViewBag.ItemTypes = new SelectList(_db.ItemTypes.ToList(), "ID", "Name");
            ViewBag.Supplier = new SelectList(_db.Suppliers.ToList(), "ID", "Name");
            ViewBag.CustomerMMD = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType==3).ToList(), "Id", "Name");
            var UserName = HttpContext.Session.GetString("UserName");
            if (UserName != "" && UserName != null)
            {
                TempData["ReservedBy"] = _db.Users.Where(x => x.UserName == UserName).FirstOrDefault().UserName;
            }
            else
            {
                TempData["ReservedBy"] = "";
            }
            PlotSale model = new PlotSale();
            if (id == 0)
            {

                ViewBag.Items = new SelectList(((from i in _db.Items
                                                 where !_db.PlotSale.Select(x => x.PlotId).Contains(i.ItemID)
                                                 select i).ToList()), "ItemID", "Description");
                model = new PlotSale();
            }
            else
            {
                model = _db.PlotSale.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotSale model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            if (model.Id == 0)
            {
                
                model.CreationDate = DateTime.Now;
                model.CompanyId =companyId;
                model.BranchId = branchId;
                model.CreatedBy = currentUser;
                model.FinancialYearId = FinancialYearId;
                _db.PlotSale.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PlotSale.Find(model.Id);
                model.CreationDate = obj.CreationDate;
                model.CreatedBy = obj.CreatedBy;
                model.CreatedUser = obj.CreatedUser;
                obj = model;
                obj.UpdationDate = DateTime.Now;
                obj.CompanyId = companyId;
                obj.BranchId = branchId;
                obj.UpdatedUser = currentUser;
                obj.FinancialYearId = FinancialYearId; 
                _db.PlotSale.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Sale", new { area = "RealEstate" });
            }
             return RedirectToAction("Index", "Sale", new { area = "RealEstate" });
            //return RedirectToAction("Index", "Sale");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotSale.Find(id);
            var ent = _db.PlotSale.Remove(entry);
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
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now.Date >= x.Fromdate && DateTime.Now.Date <= x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
