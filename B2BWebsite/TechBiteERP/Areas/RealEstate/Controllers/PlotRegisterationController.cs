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
using TechBiteERP.Model.ViewModels;

namespace TechBiteERP.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Authorize]
    public class PlotRegisterationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotRegisterationController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index(string status = "")
        {
            // var list = _db.PlotRegisterationForm.ToList();
            ViewBag.PageHeading = "Registered";
            TempData[status] = "Register";
            var list = _db.PlotSale.Join(_db.PlotRegisterationForm, ps => ps.PlotId, reg => reg.PlotId, (ps, reg) => new { Plot = ps, Registeration = reg })
.Select(s => new PlotRegisterationViewModel
{
    Id = s.Registeration.Id,
    PlotName = s.Plot.Plot.Description,
    // PlotName =( from a in _db.Items.Where(x=>x.ItemID==s.Plot.PlotId) select a.Description).FirstOrDefault(),
    Block = s.Plot.Block,
    plotStatus = Convert.ToString(s.Plot.PlotStatus.Name),
    CustomerId = s.Registeration.CustomerId,
    CustomerName = s.Registeration.CustomerName,
    CNIC = s.Registeration.CNIC,
    MobileNo = s.Registeration.MobileNo,
    DealPrice = s.Registeration.DealPrice,
    CreationDate = s.Registeration.CreationDate,
    ReservedBy = s.Plot.ReservedBy,
    ReservedFor = s.Plot.ReservedFor,
    Status = s.Registeration.Status
    //}).ToList();
//}).Where(x => x.Status == "Register").Distinct().ToList();
}).Distinct().ToList();
            if (status == "Transfer")
            {
                TempData[status] = "Transfer";

                list = _db.PlotSale.Join(_db.PlotRegisterationForm, ps => ps.PlotId, reg => reg.PlotId, (ps, reg) => new { Plot = ps, Registeration = reg })
.Select(s => new PlotRegisterationViewModel
{
    Id = s.Registeration.Id,
    PlotName = s.Plot.Plot.Description,
    // PlotName =( from a in _db.Items.Where(x=>x.ItemID==s.Plot.PlotId) select a.Description).FirstOrDefault(),
    Block = s.Plot.Block,
    plotStatus = Convert.ToString(s.Plot.PlotStatus.Name),
    CustomerId = s.Registeration.CustomerId,
    CustomerName = s.Registeration.CustomerName,
    CNIC = s.Registeration.CNIC,
    MobileNo = s.Registeration.MobileNo,
    DealPrice = s.Registeration.DealPrice,
    CreationDate = s.Registeration.CreationDate,
    ReservedBy = s.Plot.ReservedBy,
    ReservedFor = s.Plot.ReservedFor,
    Status = s.Registeration.Status
    //}).ToList();
}).Where(x => x.Status == "Transfer").Distinct().ToList();

            }


            return View(list);
        }

        public IActionResult PlotIndex(int type)
        {
            ViewBag.PageHeading = "All";
            var modelList = (from Plot in _db.PlotSale
                             join Registeration in _db.PlotRegisterationForm on Plot.PlotId equals Registeration.PlotId into PR
                             from Reg in PR.DefaultIfEmpty()
                             select new PlotRegisterationViewModel
                             {
                                 Id = Reg.Id,
                                 PlotName = Plot.Plot.Description,
                                 // PlotName =( from a in _db.Items.Where(x=>x.ItemID==s.Plot.PlotId) select a.Description).FirstOrDefault(),
                                 Block = Plot.Block,
                                 plotStatusId = Plot.PlotStatusId,
                                 plotStatus = Convert.ToString(Plot.PlotStatus.Name),
                                 CustomerId = Reg.CustomerId,
                                 CustomerName = Reg.CustomerName,
                                 CNIC = Reg.CNIC,
                                 MobileNo = Reg.MobileNo,
                                 DealPrice = Reg.DealPrice,
                                 CreationDate = Reg.CreationDate,
                                 ReservedBy = Plot.ReservedBy,
                                 ReservedFor = Plot.ReservedFor
                             }).ToList();
            if (type != 0)
            {
              
                modelList = (from Plot in _db.PlotSale
                                 join Registeration in _db.PlotRegisterationForm on Plot.PlotId equals Registeration.PlotId into PR
                                 from Reg in PR.DefaultIfEmpty()
                                 select new PlotRegisterationViewModel
                                 {
                                     Id = Reg.Id,
                                     PlotName = Plot.Plot.Description,
                                     // PlotName =( from a in _db.Items.Where(x=>x.ItemID==s.Plot.PlotId) select a.Description).FirstOrDefault(),
                                     Block = Plot.Block,
                                     plotStatusId = Plot.PlotStatusId,
                                     plotStatus = Convert.ToString(Plot.PlotStatus.Name),
                                     CustomerId = Reg.CustomerId,
                                     CustomerName = Reg.CustomerName,
                                     CNIC = Reg.CNIC,
                                     MobileNo = Reg.MobileNo,
                                     DealPrice = Reg.DealPrice,
                                     CreationDate = Reg.CreationDate,
                                     ReservedBy = Plot.ReservedBy,
                                     ReservedFor = Plot.ReservedFor
                                 }).Where(x => x.plotStatusId == type).ToList();

                var plotstatus = _db.PlotStatus.Where(x=>x.Id== type).FirstOrDefault();
                ViewBag.PageHeading = plotstatus.Name;

            }
            return View(modelList);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.State = "Create";
            ViewBag.IsMMD = _db.Roles.ToList();
            var userId = HttpContext.Session.GetString("UserId");
            var UserName = HttpContext.Session.GetString("UserName");
            if (UserName != "" && UserName != null)
            {
                TempData["LoggedBy"] = _db.Users.Where(x => x.UserName == UserName).FirstOrDefault().UserName;
            }
            var mmd = from a in _db.Users.Where(x => x.Id == userId).ToList() select a.FullName.FirstOrDefault();
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.DealValidity = new SelectList(_db.DealValidity.ToList(), "ID", "Name");
            ViewBag.DiscountFactor = new SelectList(_db.DiscountFactor.ToList(), "ID", "Name");
            ViewBag.SalesPerson = new SelectList(_db.Salesman.ToList(), "Id", "Name");
            ViewBag.MMD = new SelectList(_db.ArCustomer.Where(x => x.CustomerType == 3).ToList(), "Id", "Name");
            ViewBag.Customers = new SelectList(_db.ArCustomer.ToList(), "Id", "Name");
            PlotRegisterationForm model = new PlotRegisterationForm();
            if (id == 0)
            {
                ViewBag.Items = new SelectList(((from i in _db.Items
                                                 where !_db.PlotRegisterationForm.Select(x => x.PlotId).Contains(i.ItemID)
                                                 select i).ToList()), "ItemID", "Description");
                model = new PlotRegisterationForm();
            }
            else
            {
              //  var editmodel = _db.PlotRegisterationForm.Find(id);
                model = _db.PlotRegisterationForm.Find(id);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Transfer(int id)
        {
            ViewBag.State = "Transfer";
            ViewBag.IsMMD = _db.Roles.ToList();
            var userId = HttpContext.Session.GetString("UserId");
            var mmd = from a in _db.Users.Where(x => x.Id == userId).ToList() select a.FullName.FirstOrDefault();
            ViewBag.Block = new SelectList(_db.Block.ToList(), "ID", "Name");
            // ViewBag.Size = new SelectList(_db.Size.ToList(), "ID", "Name");
            ViewBag.Items = new SelectList(_db.Items.ToList(), "ItemID", "Description");
            ViewBag.DealValidity = new SelectList(_db.DealValidity.ToList(), "ID", "Name");
            ViewBag.DiscountFactor = new SelectList(_db.DiscountFactor.ToList(), "ID", "Name");
            ViewBag.SalesPerson = new SelectList(_db.Salesman.ToList(), "Id", "Name");
            ViewBag.MMD = new SelectList(_db.ArCustomer.Where(x => x.CustomerType == 3).ToList(), "Id", "Name");
            ViewBag.Customers = new SelectList(_db.ArCustomer.ToList(), "Id", "Name");
            PlotRegisterationForm model = new PlotRegisterationForm();
            if (id == 0)
            {
                ViewBag.Items = new SelectList(((from i in _db.Items
                                                 where !_db.PlotRegisterationForm.Select(x => x.PlotId).Contains(i.ItemID)
                                                 select i).ToList()), "ItemID", "Description");
                model = new PlotRegisterationForm();
            }
            else
            {
                model = _db.PlotRegisterationForm.Find(id);
                model.CustomerId = 0;
                model.CustomerName = "";
                model.FatherName = "";
                model.CNIC = "";
                model.Email = "";
                model.MobileNo = "";
                model.Address = "";

                model.NomineeName = "";
                model.NomineeFatherName = "";
                model.NomineeCNIC = "";
                model.NomineeEmail = "";
                model.NomineePhoneNo = "";
                model.Nomineeaddress = "";
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PlotRegisterationForm model, IFormCollection collection, IFormFile img, IFormFile img3, IFormFile Nomineeimg, IFormFile Nomineeimg4)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;
            try
            {
                if (model.CustomerId != 0)
                {
                    model.CustomerName = (from a in _db.ArCustomer.Where(x => x.Id == model.CustomerId) select a.Name).FirstOrDefault();
                }

                if (model.Id == 0)
                {
                    //var companyId = HttpContext.Session.GetInt32("CompanyId");
                    
                    if (img != null)
                    {
                        model.Photo = await UploadFile(img, "img");
                    }
                    else
                    {
                        model.Photo = "";
                    }
                    if (img3 != null)
                    {
                        model.Photo2 = await UploadFile(img3, "img");
                    }
                    else
                    {
                        model.Photo2 = "";
                    }
                    if (Nomineeimg != null)
                    {
                        model.NomineePhoto = await UploadFile(Nomineeimg, "img");
                    }
                    else
                    {
                        model.NomineePhoto = "";
                    }
                    if (Nomineeimg4 != null)
                    {
                        model.NomineePhoto2 = await UploadFile(Nomineeimg4, "img");
                    }
                    else
                    {
                        model.NomineePhoto2 = "";
                    }
                    //if(model.BlockId!=0)
                    //{
                    //    model.Block = (from b in _db.Block.Where(x => x.ID == model.BlockId) select b.Name).FirstOrDefault();
                    //}
                    model.CreationDate = DateTime.Now;
                    model.CompanyId = companyId;
                    model.BranchId = branchId;
                    model.CreatedUser = currentUser;
                    model.FinancialYearId = FinancialYearId;
                    model.Status = "Register";
                    _db.PlotRegisterationForm.Add(model);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var obj = _db.PlotRegisterationForm.Find(model.Id);
                   model.CreatedUser = obj.CreatedUser;
                     model.CreationDate = obj.CreationDate;
                    obj = model;
                    if (img != null)
                    {
                        obj.Photo = await UploadFile(img, "img");
                    }
                    if (img3 != null)
                    {
                        obj.Photo2 = await UploadFile(img3, "img");
                    }
                    if (Nomineeimg != null)
                    {
                        obj.NomineePhoto = await UploadFile(Nomineeimg, "img");
                    }
                    if (Nomineeimg4 != null)
                    {
                        obj.NomineePhoto2 = await UploadFile(Nomineeimg4, "img");
                    }
                    //if (model.BlockId != 0)
                    //{
                    //    obj.Block = (from b in _db.Block.Where(x => x.ID == model.BlockId) select b.Name).FirstOrDefault();
                    //}
                    obj.Status = "Register";
                    obj.UpdationDate = DateTime.Now;
                    obj.CompanyId = companyId;
                    obj.BranchId = branchId;
                    obj.UpdatedUser = currentUser;
                    _db.PlotRegisterationForm.Update(obj);
                    await _db.SaveChangesAsync();

                }
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "PlotRegisteration");
            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Index", "PlotRegisteration");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transfer(PlotRegisterationForm model, IFormCollection collection, IFormFile img, IFormFile img3, IFormFile Nomineeimg, IFormFile Nomineeimg4)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var branchId = HttpContext.Session.GetInt32("BranchId").Value;
            var FinancialYearId = HttpContext.Session.GetInt32("FinancialYearId").Value;

            if (model.CustomerId != 0)
            {
                model.CustomerName = (from a in _db.ArCustomer.Where(x => x.Id == model.CustomerId) select a.Name).FirstOrDefault();
            }
            try
            {
                //Get Registeration Data and insert into Tranfer Table
                var objEdit = _db.PlotRegisterationForm.Find(model.Id);
                var obj = new PlotTransferHistory();
                if (img != null)
                {
                    obj.Photo = await UploadFile(img, "img");
                }
                if (img3 != null)
                {
                    obj.Photo2 = await UploadFile(img3, "img");
                }
                if (Nomineeimg != null)
                {
                    obj.NomineePhoto = await UploadFile(Nomineeimg, "img");
                }
                if (Nomineeimg4 != null)
                {
                    obj.NomineePhoto2 = await UploadFile(Nomineeimg4, "img");
                }
                obj.Id = 0;
                obj.BlockId = objEdit.BlockId;
                obj.PlotId = objEdit.PlotId;
                obj.Size = objEdit.Size;
                obj.Dimensions = objEdit.Dimensions;
                obj.Factors = objEdit.Factors;
                obj.PlotPrice = objEdit.PlotPrice;
                obj.CustomerId = model.CustomerId;
                obj.ToCustomerId = objEdit.CustomerId;
                obj.CustomerName = model.CustomerName;
                obj.FatherName = model.FatherName;
                obj.CNIC = model.CNIC;
                obj.Email = model.Email;
                obj.MobileNo = model.MobileNo;
                obj.Address = model.Address;
                obj.NomineeName = model.NomineeName;
                obj.NomineeFatherName = model.NomineeFatherName;
                obj.NomineeCNIC = model.NomineeCNIC;
                obj.NomineeEmail = model.NomineeEmail;
                obj.NomineePhoneNo = model.NomineePhoneNo;
                obj.Nomineeaddress = model.Nomineeaddress;
                obj.FormNo = objEdit.FormNo;
                obj.DiscountPer = objEdit.DiscountPer;
                obj.DiscountAmount = objEdit.DiscountAmount;
                obj.RegisterCharges = objEdit.RegisterCharges;
                obj.Installments = objEdit.Installments;
                obj.DealPrice = objEdit.DealPrice;
                obj.DealValidity = objEdit.DealValidity;
                obj.DiscountFactor = objEdit.DiscountFactor;
                obj.SalesPerson = objEdit.SalesPerson;
                obj.MMD = objEdit.MMD;
                obj.LoggedBy = objEdit.LoggedBy;
                obj.MMDPercentage = objEdit.MMDPercentage;
                obj.MMDPercentageAmount = objEdit.MMDPercentageAmount;
                obj.WHTAmount = objEdit.WHTAmount;
                obj.NetPayable = objEdit.NetPayable;
                obj.CreationDate = model.CreationDate;
                obj.TransferDate = DateTime.Now;
                obj.CreatedBy = HttpContext.Session.GetString("UserId");
                obj.CreationDate = DateTime.Now;
                obj.Status ="Transfer";
                obj.BranchId = objEdit.BranchId;
                obj.CompanyId = objEdit.CompanyId;
                obj.Canceled = objEdit.Canceled;
                obj.TransferDate = DateTime.Now;
                model.CompanyId = companyId;
                model.BranchId = branchId;
                model.CreatedUser = currentUser;
                model.FinancialYearId = FinancialYearId;
                model.Status = "Register";
                _db.PlotTransferHistory.Add(obj);
                await _db.SaveChangesAsync();

                //Update Registered To Transfer
               
               // objEdit = model;
                if (img != null)
                {
                    objEdit.Photo = await UploadFile(img, "img");
                }
                if (img3 != null)
                {
                    objEdit.Photo2 = await UploadFile(img3, "img");
                }
                if (Nomineeimg != null)
                {
                    objEdit.NomineePhoto = await UploadFile(Nomineeimg, "img");
                }
                if (Nomineeimg4 != null)
                {
                    objEdit.NomineePhoto2 = await UploadFile(Nomineeimg4, "img");
                }
                objEdit.Status = "Transfer";
                obj.TransferDate = DateTime.Now;
                _db.PlotRegisterationForm.Update(objEdit);
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
               
            }
            // return RedirectToAction("Index", "Item", new { area = "" });
            return RedirectToAction("Transfer", "PlotRegisteration");
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
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TechBiteImage\\PlotRegistration", fileName);
                        using (var Fstream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(Fstream);

                            var fullPath = "/TechBiteImage/PlotRegistration/" + fileName;
                            filesList += fullPath;
                        }
                    }
                }
            }
            else if (type == "file")
            {
                if (img != null)
                {
                    if (img.Length > 0)
                    {
                        var fileName = Path.GetFileName(img.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\HR\\employee-Qualification", fileName);
                        using (var Fstream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(Fstream);

                            var fullPath = "/uploads/HR/employee-Qualification/" + fileName;
                            filesList += fullPath;
                        }
                    }
                }
            }
            return filesList;
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entry = _db.PlotRegisterationForm.Find(id);
            var ent = _db.PlotRegisterationForm.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult getCustomerDetail(int id)
        {
            var item = _db.ArCustomer.Find(id);
            return Ok(item);

        }
        public IActionResult getItemPrice(int id)
        {
            var item = _db.PriceList.Where(x => x.ItemID == id && DateTime.Now > x.Fromdate && DateTime.Now < x.Todate).FirstOrDefault();
            return Ok(item);
        }
    }
}
