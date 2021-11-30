using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Areas.HumanResource.Controllers
{
    [Area("HumanResource")]
    [Authorize]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BranchController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public int GetMaxNo()
        {
            int maxNo = 1;
            var entry = _db.Branch.ToList();
            if (entry.Count > 0)
            {
                maxNo = entry.Max(x => Convert.ToInt32(x.POSCode)) + 1;
            }
            return maxNo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _db.Branch.ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Region = new SelectList(_db.Regions.ToList(), "RegionID", "RegionName");
            ViewBag.BranchType = new SelectList(_db.BranchType.ToList(), "ID", "Name");
            Branch model = new Branch();
            if (id == 0)
            {
                model = new Branch();
                model.POSCode = GetMaxNo();
            }
            else
            {
                model = _db.Branch.Find(id);
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Branch model, IFormCollection collection, IFormFile img)
        {

            if (model.ID == 0)
            {
                if (img != null)
                {
                    model.Photo = await UploadFile(img, "img");
                }
                else
                {
                    model.Photo = "";
                }
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                _db.Branch.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Branch.Find(model.ID);
                obj = model;
                if (img != null)
                {
                    obj.Photo = await UploadFile(img, "img");
                }
                else
                {
                    obj.Photo = "";
                }
                obj.CreationDate = DateTime.Now;
                obj.CompanyId = 1;
                _db.Branch.Update(obj);
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<string> UploadFile(IFormFile img, string type)
        {
            string filesList = "";
            if (type == "img")
            {
                if (img != null)
                {
                    if (img.Length > 0)
                    {
                        var fileName = Path.GetFileName(img.FileName);
                        //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\HR\\employee-images", fileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TechBiteImage\\Branch", fileName);
                        using (var Fstream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(Fstream);

                            var fullPath = "/TechBiteImage/Branch/" + fileName;
                            filesList += fullPath;
                        }
                    }
                }
            }
           
            return filesList;
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Branch.Find(id);
            var ent = _db.Branch.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
