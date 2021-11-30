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
    public class ApplicationReportController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationReportController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var re = _db.ApplicationReports.ToList();
            return View(re);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Reports = new SelectList(_db.ApplicationReports.ToList(), "ID", "Name");
            ApplicationReports model = new ApplicationReports();
            if (id == 0)
            {
                model = new ApplicationReports();
            }
            else
            {
                model = _db.ApplicationReports.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationReports model, IFormCollection collection)
        {

            if (model.ID == 0)
            {
                _db.ApplicationReports.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.ApplicationReports.Find(model.ID);
                obj = model;
                _db.ApplicationReports.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.ApplicationReports.Find(id);
            var ent = _db.ApplicationReports.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
