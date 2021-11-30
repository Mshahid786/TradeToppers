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

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class BalanceOpeningController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BalanceOpeningController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.OpeningBalance.ToList();
            return View(model);
        }
       
        public int GetMaxNo()
        {
            int maxNo = 1;
            var entry = _db.OpeningBalance.ToList();
            if (entry.Count >0)
            {
                maxNo = entry.Max(x=>x.Id) + 1;
            }
            return maxNo;
        }
        [HttpGet]

        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.ToList(), "VoucherTypesID", "Type");
            ViewBag.WorkingYear = new SelectList(_db.FinYear.ToList(), "Id", "FYear");
            ViewBag.branches = new SelectList(_db.Branch.ToList(), "ID", "POSName");
            ViewBag.Accounts = new SelectList((from s in _db.GLAccounts.Where(x => x.IsDeleted == false && x.IsActive == true && x.CompanyId == 1)
                                               select new
                                               {
                                                   Id = s.Id,
                                                   Name = s.Code + " - " + s.Name
                                               }).ToList(), "Id", "Name");
            OpeningBalance model = new OpeningBalance();
            if (id == 0)
            {
                TempData["No"] = GetMaxNo();
                model = new OpeningBalance();
            }
            else
            {
               
                model = _db.OpeningBalance.Find(id);
                TempData["No"] =model.Id ;
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(OpeningBalance model, IFormCollection collection)
        {
            ViewBag.VoucherTypes = new SelectList(_db.VoucherTypes.ToList(), "VoucherTypesID", "Type");
            if (model.Id == 0)
            {
                model.CreatedDate = DateTime.Now;
                _db.OpeningBalance.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.OpeningBalance.Find(model.Id);
                obj = model;
                obj.CreatedDate = DateTime.Now;
                _db.OpeningBalance.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.OpeningBalance.Find(id);
            var ent = _db.OpeningBalance.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
