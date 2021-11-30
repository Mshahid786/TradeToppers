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
    public class PayTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PayTypeController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _db.PayType.ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            PayType model = new PayType();
            if (id == 0)
            {
                model = new PayType();
            }
            else
            {
                model = _db.PayType.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PayType model, IFormCollection collection)
        {

            if (model.ID == 0)
            {
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.PayType.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.PayType.Find(model.ID);
                obj = model;
                obj.CreationDate = DateTime.Now;
                obj.CompanyId = 1;
                obj.BranchId = 1;
                _db.PayType.Update(obj);
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
