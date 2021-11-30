using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Authorize]
    public class RegisterationFormController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RegisterationFormController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]

        public IActionResult Index()
        {
            var list = _db.RegisterationForms.Include(x=>x.Branch).ToList();
             
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
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;

            if (UserName != "" && UserName != null)
            {
                ViewBag.DeliveredBy = new SelectList(_db.Users.Where(x => x.UserName == UserName).ToList(), "Id", "UserName");
            }
            else
            {
                ViewBag.DeliveredBy = null;
            }

            ViewBag.Branches = new SelectList(_db.Branch.Where(x=>x.CompanyId == companyId).ToList(), "ID", "POSName");
            
            RegisterationForms model = new RegisterationForms();
            if (id == 0)
            {
                model = new RegisterationForms();
 
            }
            else
            {
                model = _db.RegisterationForms.Find(id);      
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterationForms model, IFormCollection collection)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var BranchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            if (model.Id == 0)
            {
                
                model.CreationDate = DateTime.Now;
                model.CompanyId = companyId;
                model.BranchId = BranchId;
                model.CreatedUser = currentUser;
                model.FinancialYearId = FinancialYearId;
                _db.RegisterationForms.Add(model);
                await _db.SaveChangesAsync();

                
                for (int i = model.FormNoStart; i <= model.FormNoEnd; i++)
                {
                    RegisterationFormDetail modeldetail = new RegisterationFormDetail();
                    modeldetail.RegisterationFormId = model.Id;
                    modeldetail.FormNo = i;
                    modeldetail.FormCode = string.Concat(model.FormCode,i);
                    modeldetail.BranchId = BranchId;
                    modeldetail.RegisterationFormId = companyId;
                    _db.RegisterationFormsDetail.Add(modeldetail);
                    await _db.SaveChangesAsync();
                }
               
            }
            else
            {
                var obj = _db.RegisterationForms.Find(model.Id);
                model.CreationDate = obj.CreationDate;
                model.CreatedUser = obj.CreatedUser;
                obj = model;
                obj.UpdationDate = DateTime.Now;
                obj.CompanyId = companyId;
                obj.BranchId = BranchId;
                obj.UpdatedUser = currentUser;
                _db.RegisterationForms.Update(obj);
                await _db.SaveChangesAsync();

                //Delete Detail
                var items = _db.RegisterationFormsDetail.Where(x => x.RegisterationFormId == model.Id);
                foreach(var item in items)
                {
                    var detail = _db.RegisterationFormsDetail.Find(item.Id);
                    _db.RegisterationFormsDetail.Remove(detail);
                    await _db.SaveChangesAsync();
                }

                var modeldetail = new RegisterationFormDetail();
                for (int i = model.FormNoStart; i <= model.FormNoEnd; i++)
                {
                    modeldetail.RegisterationFormId = model.Id;
                    modeldetail.FormNo = i;
                    modeldetail.FormCode = string.Concat(model.FormCode, i);
                    modeldetail.BranchId = BranchId;
                    modeldetail.RegisterationFormId = companyId;
                    _db.RegisterationFormsDetail.Add(modeldetail);
                    await _db.SaveChangesAsync();
                }

            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "RegisterationForm");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.RegisterationForms.Find(id);
            var ent = _db.RegisterationForms.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult getRegisterationDetail(int PlotId,int blockId)
        //{
        //    var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId && x.BlockId==blockId).FirstOrDefault();
        //    return Ok(item);

        //}
        public IActionResult getRegisterationDetail(int PlotId)
        {
            var item = _db.PlotRegisterationForm.Where(x=>x.PlotId==PlotId ).FirstOrDefault();
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x=>x.ItemID ==id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
