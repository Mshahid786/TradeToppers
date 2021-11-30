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

namespace TechBiteERP.Areas.AccountReceivable.Controllers
{
    [Area("HumanResource")]
    [Authorize]
    public class PriceListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PriceListController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var salesMan = _db.PriceList.Where(x=>x.Deleted==false).ToList();
            return View(salesMan);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.PriceCategory = new SelectList(_db.PriceCategories.ToList(), "ID", "Name");
            ViewBag.Packings = new SelectList(_db.Packings.ToList(), "ID", "Name");
            ViewBag.Brands = new SelectList(_db.Brands.ToList(), "ID", "Name");

            ViewBag.District = new SelectList(_db.Districts.ToList(), "DistrictID", "DistrictName");
            ViewBag.Area = new SelectList(_db.Areas.ToList(), "AreaID", "AreaName");
            ViewBag.City = new SelectList(_db.Cities.ToList(), "CityID", "CityName");
            ViewBag.Country = new SelectList(_db.Countries.ToList(), "CountryID", "CountryName");
            ViewBag.Province = new SelectList(_db.Provinces.ToList(), "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(_db.Regions.ToList(), "RegionID", "RegionName");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            PriceList model = new PriceList();
            if (id==0)
            {
                 model = new PriceList();
                model.Fromdate = DateTime.Now;
                model.Todate = DateTime.Now;
            }
            else
            {
                model = _db.PriceList.Find(id);
            }
            
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PriceList model,IFormCollection collection)
        {

            if (model.Id == 0)
            {
                _db.PriceList.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PriceList.Find(model.Id);
                obj = model;
                _db.PriceList.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PriceList.Find(id);
            entry.Deleted = true;
            var ent = _db.PriceList.Update(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
