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
    public class SalesManController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SalesManController (ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var salesMan = _db.Salesman.Where(x=>x.Deleted==false).ToList();
            return View(salesMan);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.District = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Area = new SelectList(_db.Areas.ToList(), "Id", "ConfigName");
            ViewBag.City = new SelectList(_db.Cities.ToList(), "Id", "ConfigName");
            ViewBag.Country = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Province = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            ViewBag.Region = new SelectList(_db.AppValues.ToList(), "Id", "ConfigName");
            Salesman model = new Salesman();
            if (id==0)
            {
                 model = new Salesman();
            }
            else
            {
                model = _db.Salesman.Find(id);
            }
            
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Salesman model,IFormCollection collection)
        {

            if (model.Id == 0)
            {
                _db.Salesman.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Salesman.Find(model.Id);
                obj = model;
                _db.Salesman.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Salesman.Find(id);
            entry.Deleted = true;
            var ent = _db.Salesman.Update(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
