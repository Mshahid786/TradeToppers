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

namespace TechBiteERP.Areas.HumanResource.Controllers
{
    [Area("HumanResource")]
    [Authorize]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CurrencyController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public int GetMaxNo()
        {
            int maxNo = 1;
            var entry = _db.Currency.ToList();
            if (entry.Count > 0)
            {
                maxNo = entry.Max(x => Convert.ToInt32(x.Code)) + 1;
            }
            return maxNo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _db.Currency.ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            Currency model = new Currency();
            if (id == 0)
            {
                model = new Currency();
                model.Code = GetMaxNo();
            }
            else
            {
                model = _db.Currency.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Currency model, IFormCollection collection)
        {

            if (model.ID == 0)
            {
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.Currency.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Currency.Find(model.ID);
                obj = model;
                obj.CreationDate = DateTime.Now;
                obj.CompanyId = 1;
                obj.BranchId = 1;
                _db.Currency.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Currency.Find(id);
            var ent = _db.Currency.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
