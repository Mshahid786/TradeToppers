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
    [Area("AccountReceivable")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var salesMan = _db.ArCustomer.Where(x=>x.Deleted==false).ToList();
            return View(salesMan);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.District = new SelectList(_db.Districts.ToList(), "DistrictID", "DistrictName");
            ViewBag.Area = new SelectList(_db.Areas.ToList(), "AreaID", "AreaName");
            ViewBag.City = new SelectList(_db.Cities.ToList(), "CityID", "CityName");
            ViewBag.Country = new SelectList(_db.Countries.ToList(), "CountryID", "CountryName");
            ViewBag.Province = new SelectList(_db.Provinces.ToList(), "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(_db.Regions.ToList(), "RegionID", "RegionName");
            ViewBag.SalesMan = new SelectList(_db.Salesman.ToList(), "Id", "Name");
            ViewBag.Accounts = new SelectList(_db.GLAccounts.ToList(), "Id", "Name");
            ArCustomer model = new ArCustomer();
            if (id==0)
            {
                 model = new ArCustomer();
            }
            else
            {
                model = _db.ArCustomer.Find(id);
            }
            
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ArCustomer model,IFormCollection collection)
        {

            if (model.Id == 0)
            {
                _db.ArCustomer.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.ArCustomer.Find(model.Id);
                obj = model;
                _db.ArCustomer.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.ArCustomer.Find(id);
            //entry.Deleted = true;
            var ent = _db.ArCustomer.Update(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
