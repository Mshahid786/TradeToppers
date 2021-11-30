using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Areas.RealEstate.Controllers
{
    [Area("HumanResource")]
    [Authorize]
    public class PriceListNewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PriceListNewController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.PriceListNew.ToList();
             
            return View(list);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.CompanyName = HttpContext.Session.GetString("CompanyName");
            ViewBag.BranchId = HttpContext.Session.GetInt32("BranchId");
            var UserId = HttpContext.Session.GetString("UserId");
            var UserName = HttpContext.Session.GetString("UserName");

            ViewBag.Colors = new SelectList(_db.Colors.ToList(), "ID", "Name");
            // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            // ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            PriceListNew model = new PriceListNew();
            if (id == 0)
            {
                model = new PriceListNew();
               // model.BookingDate = DateTime.Now;   
            }
            else
            {
                model = _db.PriceListNew.Find(id);
                var items = _db.PriceListNewDetail.Where(i => i.PriceListNewId == id).ToList();
                model.PriceListDetails = items;
                
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PriceListNew model, IFormCollection collection)
        {
            PriceListNewDetail[] modelItems = JsonConvert.DeserializeObject<PriceListNewDetail[]>(collection["tblPaymentPlanDetails"]);
            if (model.Id == 0)
            {
                //var companyId = HttpContext.Session.GetInt32("CompanyId");
                model.CreationDate = DateTime.Now;
                model.CompanyId = 1;
                model.BranchId = 1;
                _db.PriceListNew.Add(model);
                  _db.SaveChanges();

                foreach (var item in modelItems)
                {
                    PriceListNewDetail detail = new PriceListNewDetail();
                    detail.PriceListNewId = model.Id;
                    detail.Sr = item.Sr;
                    detail.MRPrice = item.MRPrice;
                    detail.TradePrice = item.TradePrice;
                    detail.RP = item.RP;
                    detail.DiscountPer = item.DiscountPer;
                    detail.StartDate = item.StartDate;
                    detail.EndDate = item.EndDate;
                    detail.Comments = item.Comments;
                    _db.PriceListNewDetail.Add(detail);
                      _db.SaveChanges();
                }
            }
            else
            {
                var obj = _db.PriceListNew.Find(model.Id);
                obj = model;
                _db.PriceListNew.Update(obj);
                await _db.SaveChangesAsync();

                //Delete Previous entry
                var entry = _db.PriceListNewDetail.Where(x => x.PriceListNewId == obj.Id);
                foreach (var dt in entry)
                {
                     _db.PriceListNewDetail.Remove(dt);
                   // _db.PriceListNewDetails.Remove(entrydelete);
                    
                }
                _db.SaveChanges();
                //Insert Detail
                foreach (var item in modelItems)
                {
                   

                    PriceListNewDetail detail = new PriceListNewDetail();
                    //var detail = _db.PriceListNewDetails.Find(item.PlanDetailId);
                    //if (detail != null)
                    //{
                    //    detail.PriceListNewId = model.Id;
                    //    detail.Sr = item.Sr;
                    //    detail.DueDate = item.DueDate;
                    //    detail.Amount = item.Amount;

                    //    _db.PriceListNewDetails.Update(detail);
                    //    await _db.SaveChangesAsync();
                    //}
                    //else
                    //{

                    detail = new PriceListNewDetail();
                        detail.PriceListNewId = model.Id;
                        detail.Sr = item.Sr;
                    detail.MRPrice = item.MRPrice;
                    detail.TradePrice = item.TradePrice;
                    detail.RP = item.RP;
                    detail.DiscountPer = item.DiscountPer;
                    detail.StartDate = item.StartDate;
                    detail.EndDate = item.EndDate;
                    detail.Comments = item.Comments;
                    _db.PriceListNewDetail.Add(detail);
                        await _db.SaveChangesAsync();

                    //}

                }

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PriceListNew");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PriceListNew.Find(id);
            var ent = _db.PriceListNew.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult getRegisterationDetail(int PlotId,int blockId)
        {
            var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId && x.BlockId==blockId).FirstOrDefault();
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
