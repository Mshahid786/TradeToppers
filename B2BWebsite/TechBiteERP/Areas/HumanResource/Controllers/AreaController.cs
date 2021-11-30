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
    public class AreaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AreaController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.Areas.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Country = new SelectList(_db.Countries.ToList(), "CountryID", "CountryName");
            Area model = new Area();
            if (id == 0)
            {
                model = new Area();
            }
            else
            {
                model = _db.Areas.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Area model, IFormCollection collection)
        {

            if (model.AreaID == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.Areas.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Areas.Find(model.AreaID);
                obj.AreaName = model.AreaName;
                _db.Areas.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Areas.Find(id);
            var ent = _db.Areas.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
