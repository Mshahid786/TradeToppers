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
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CountryController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var country = _db.Countries.ToList();
            return View(country);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.District = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Area = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.City = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Country = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Province = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Region = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            Country model = new Country();
            if (id == 0)
            {
                model = new Country();
            }
            else
            {
                model = _db.Countries.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Country model, IFormCollection collection)
        {

            if (model.CountryID == 0)
            {
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.Countries.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Countries.Find(model.CountryID);
                obj = model;
                obj.CreationDate = DateTime.Now;
                obj.CompanyId = 1;
                obj.BranchId = 1;
                _db.Countries.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Countries.Find(id);
            var ent = _db.Countries.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
