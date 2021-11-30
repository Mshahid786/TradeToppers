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
    public class FinYearController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FinYearController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.FinYear.ToList();
            return View(model);
        }
       
        public int GetMaxNo()
        {
            int maxNo = 1;
            var entry = _db.FinYear.ToList();
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
            FinYear model = new FinYear();
            if (id == 0)
            {
                TempData["No"] = GetMaxNo();
                model = new FinYear();
            }
            else
            {
               
                model = _db.FinYear.Find(id);
                TempData["No"] =model.Id ;
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(FinYear model, IFormCollection collection)
        { 
            if (model.Id == 0)
            {
                _db.FinYear.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.FinYear.Find(model.Id);
                obj = model;
                _db.FinYear.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.FinYear.Find(id);
            var ent = _db.FinYear.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
