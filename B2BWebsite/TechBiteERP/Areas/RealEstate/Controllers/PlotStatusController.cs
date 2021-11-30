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

namespace TechBiteERP.PlotStatuss.HumanResource.Controllers
{
    [Area("RealEstate")]
    [Authorize]
    public class PlotStatusController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotStatusController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.PlotStatus.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";

            PlotStatus model = new PlotStatus();
            if (id == 0)
            {
                model = new PlotStatus();
            }
            else
            {
                model = _db.PlotStatus.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotStatus model, IFormCollection collection)
        {

            if (model.Id == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.PlotStatus.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PlotStatus.Find(model.Id);
                obj.Name = model.Name;
                _db.PlotStatus.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotStatus.Find(id);
            var ent = _db.PlotStatus.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
