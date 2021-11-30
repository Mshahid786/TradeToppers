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

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class VoucherTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VoucherTypeController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.VoucherTypes.ToList();
            return View(model);
        }
       
        public int GetMaxNo()
        {
            int maxNo = 1;
            var entry = _db.VoucherTypes.ToList();
            if (entry.Count >0)
            {
                maxNo = entry.Max(x=>x.VoucherTypesID) + 1;
            }
            return maxNo;
        }
        [HttpGet]

        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
 
            VoucherType model = new VoucherType();
            if (id == 0)
            {
                TempData["No"] = GetMaxNo();
                model = new VoucherType();
            }
            else
            {
               
                model = _db.VoucherTypes.Find(id);
                TempData["No"] =model.VoucherTypesID ;
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(VoucherType model, IFormCollection collection)
        {

            if (model.VoucherTypesID == 0)
            {
                _db.VoucherTypes.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.VoucherTypes.Find(model.VoucherTypesID);
                obj = model;
                _db.VoucherTypes.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.VoucherTypes.Find(id);
            var ent = _db.VoucherTypes.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
