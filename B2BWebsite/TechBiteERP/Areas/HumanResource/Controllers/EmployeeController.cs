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
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.HREmployees.ToList();
            return View(list);
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
            HREmployee model = new HREmployee();
            if (id == 0)
            {
                model = new HREmployee();
            }
            else
            {
                model = _db.HREmployees.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(HREmployee model, IFormCollection collection)
        {

            if (model.EmployeeID == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyID = 1;
                model.BranchID = 1;
                _db.HREmployees.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.HREmployees.Find(model.EmployeeID);
                obj.EmployeeName = model.EmployeeName;
                obj.FatherName = model.FatherName;
                obj.Email = model.Email;
                obj.DateOFBirth = model.DateOFBirth;
                obj.CityID = model.CityID;
                obj.CellNo1 = model.CellNo1;
                obj.CellNo2 = model.CellNo2;
                obj.CreationDate = model.CreationDate;
                obj.Address = model.Address;
                obj.CountryId = model.CountryId;
                _db.HREmployees.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.HREmployees.Find(id);
            var ent = _db.HREmployees.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
