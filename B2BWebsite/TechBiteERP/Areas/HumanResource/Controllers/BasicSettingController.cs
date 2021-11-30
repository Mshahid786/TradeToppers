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
    public class BasicSettingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BasicSettingController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.BasicSettings.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Accounts = new SelectList((from s in _db.GLAccounts.Where(x => x.IsDeleted == false && x.IsActive == true && x.CompanyId == 1)
                                               select new
                                               {
                                                   Id = s.Id,
                                                   Name = s.Code + " - " + s.Name
                                               }).ToList(), "Id", "Name");
            ViewBag.Vtype = new SelectList(_db.VoucherTypes.Where(x=>x.VoucherTypesID!=1).ToList(),"VoucherTypesID","Type");

 
            BasicSetting model = new BasicSetting();
            if (id == 0)
            {
                model = new BasicSetting();
            }
            else
            {
                model = _db.BasicSettings.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BasicSetting model, IFormCollection collection)
        {

            if (model.Id == 0)
            {
                var list = _db.BasicSettings.ToList();
                if (list.Count == 0)
                {
                    _db.BasicSettings.Add(model);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var obj = _db.BasicSettings.Find(1);
                    obj = model;
                    _db.BasicSettings.Update(obj);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var obj = _db.BasicSettings.Find(model.Id);
                obj = model;
                _db.BasicSettings.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction("Create", new { id = 1 });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.BasicSettings.Find(id);
            var ent = _db.BasicSettings.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
