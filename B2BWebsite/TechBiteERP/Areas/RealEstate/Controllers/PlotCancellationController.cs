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
    public class PlotCancellationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotCancellationController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.PlotCancellation.ToList();
             
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
            ViewBag.ActionType = new SelectList(_db.ActionType.ToList(), "ID", "Name");
            ViewBag.PlotStatus = new SelectList(_db.PlotStatus.ToList(), "Id", "Name");
            if (UserName != "" && UserName == null)
            {
             ViewBag.DeliveredBy = new SelectList(_db.Users.Where(x => x.UserName == UserName).ToList(), "Id", "UserName");
            }
                ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            ViewBag.CustomerMMD = new SelectList(_db.ArCustomer.Where(x=>x.CustomerType==3).ToList(), "Id", "Name");
            PlotCancellation model = new PlotCancellation();
            if (id == 0)
            {

                model = new PlotCancellation();
                model.CourierDate = DateTime.Now;
                model.LogDate = DateTime.Now;
            }
            else
            {
                model = _db.PlotCancellation.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotCancellation model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            if (model.Id == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.PlotCancellation.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PlotCancellation.Find(model.Id);
                obj = model;
                //obj.UpdationDate = DateTime.Now;
                //obj.CompanyId = companyId;
                //obj.BranchId = branchId;
                //obj.UpdatedUser = currentUser;
                _db.PlotCancellation.Update(obj);
                await _db.SaveChangesAsync();

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PlotCancellation");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotCancellation.Find(id);
            var ent = _db.PlotCancellation.Remove(entry);
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
