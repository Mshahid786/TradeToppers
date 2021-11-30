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
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CityController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _db.Cities.ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Country = new SelectList(_db.Countries.ToList(), "CountryID", "CountryName");
            ViewBag.Province = new SelectList(_db.Provinces.ToList(), "ProvinceID", "ProvinceName");
            City model = new City();
            if (id == 0)
            {
                model = new City();
            }
            else
            {
                model = _db.Cities.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(City model, IFormCollection collection)
        {

            if (model.CityID == 0)
            {
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.Cities.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Cities.Find(model.CityID);
                obj = model;
                obj.CreationDate = DateTime.Now;
                obj.CompanyId = 1;
                obj.BranchId = 1;
                _db.Cities.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Cities.Find(id);
            var ent = _db.Cities.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
