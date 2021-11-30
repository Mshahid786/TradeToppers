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

namespace TechBiteERP.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.Items.ToList();
            return View(list);
        }

        public int getMaxNo()
        {
            int maxNo = 1;
            var invoices = _db.Items.ToList();
            if (invoices.Count > 0)
            {
                maxNo = invoices.Max(v =>Convert.ToInt32(v.ItemCode));
                maxNo = maxNo + 1;
            }
            else
            {
#pragma warning disable CS1717 // Assignment made to same variable
                maxNo = maxNo;
#pragma warning restore CS1717 // Assignment made to same variable
            }

            return maxNo;

        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            ViewBag.Colors = new SelectList(_db.Colors.ToList(), "ID", "Name");
           // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            ViewBag.ItemClass = new SelectList(_db.ItemClasses.ToList(), "ID", "Name");
            ViewBag.PriceCategory = new SelectList(_db.PriceCategories.ToList(), "ID", "Name");
            ViewBag.ItemTypes = new SelectList(_db.ItemTypes.ToList(), "ID", "Name");
            ViewBag.Units = new SelectList(_db.Uoms.ToList(), "ID", "Name");
            ViewBag.Packings = new SelectList(_db.Packings.ToList(), "ID", "Name");
            ViewBag.Category = new SelectList(_db.Categories.ToList(), "ID", "Name");
            ViewBag.MasterItems = new SelectList(_db.MasterItems.ToList(), "ID", "Name");
            ViewBag.Origins = new SelectList(_db.Origins.ToList(), "ID", "Name");
            ViewBag.Brands = new SelectList(_db.Brands.ToList(), "ID", "Name");
            ViewBag.Supplier = new SelectList(_db.Suppliers.ToList(), "ID", "Name");
            ViewBag.Accounts = new SelectList((from s in _db.GLAccounts.Where(x => x.IsDeleted == false && x.IsActive == true && x.CompanyId == 1)
                                               select new
                                               {
                                                   Id = s.Id,
                                                   Name = s.Code + " - " + s.Name
                                               }).ToList(), "Id", "Name");
            Item model = new Item();
            if (id == 0)
            {
                TempData["MaxNo"] =string.Concat("000", getMaxNo());
                model = new Item();
            }
            else
            {
                
                model = _db.Items.Find(id);
                TempData["MaxNo"] =  model.ItemCode;
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Item model, IFormCollection collection)
        {

            if (model.ItemID == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.Companyid = 1;
                model.Branchid = 1;
                if (model.Block != 0)
                {
                    model.BlockID = (from a in _db.Block.Where(x => x.ID == model.Block) select a.Name).FirstOrDefault();
                }
                else
                {
                    model.BlockID = "";
                }
                    _db.Items.Add(model);
                await _db.SaveChangesAsync();
            }
            else
            {
                var obj = _db.Items.Find(model.ItemID);
                obj = model;
                if (model.Block != 0)
                {
                    obj.BlockID = (from a in _db.Block.Where(x => x.ID == model.Block) select a.Name).FirstOrDefault();
                }
                else
                {
                    obj.BlockID = "";
                }
                _db.Items.Update(obj);
                await _db.SaveChangesAsync();

            }
           // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "Item");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.Items.Find(id);
            var ent = _db.Items.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult getblockName(int id)
        {
            var item = _db.Block.Where(x => x.ID == id).FirstOrDefault();
            return Ok(item);
        }
    }
}
