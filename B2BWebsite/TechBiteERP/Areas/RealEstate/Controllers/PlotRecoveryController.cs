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
    public class PlotRecoveryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotRecoveryController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.PlotRecovery.ToList();
             
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
            ViewBag.DeliveredVia = new SelectList(_db.DeliveredVia.ToList(), "ID", "Name");
            ViewBag.CustomerMMD = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType==3).ToList(), "Id", "Name");
            PlotRecovery model = new PlotRecovery();
            if (id == 0)
            {
                model = new PlotRecovery();
            }
            else
            {
                model = _db.PlotRecovery.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotRecovery model, IFormCollection collection)
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
                _db.PlotRecovery.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PlotRecovery.Find(model.Id);
                 //obj = model;
                obj.UpdationDate = DateTime.Now;
                obj.FromDate = model.FromDate; 
                obj.ToDate = model.ToDate; 
                obj.PlotId = model.PlotId; 
                obj.Block = model.Block; 
                obj.Client = model.Client; 
                obj.Stations = model.Stations; 
                obj.LoginId = model.LoginId; 
                obj.FormNo = model.FormNo; 
                obj.BranchId = branchId;
                obj.UpdatedUser = currentUser;
                _db.PlotRecovery.Update(obj);
                await _db.SaveChangesAsync();

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PlotRecovery");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotRecovery.Find(id);
            var ent = _db.PlotRecovery.Remove(entry);
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
