using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using TechBiteERP.Model.Models;
using Microsoft.AspNetCore.Authorization;
using TechBiteERP.Data;

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class ProductBalanceOpeningController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;

        [Obsolete]
        public ProductBalanceOpeningController(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        public ActionResult File()
        {
            FileUploadViewModel model = new FileUploadViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult File(FileUploadViewModel model)
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + model.XlsFile.FileName;

            FileInfo file = new FileInfo(Path.Combine(rootFolder + "\\ProductBalanceOpening", fileName));
            using (var stream = new MemoryStream())
            {
                model.XlsFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    package.SaveAs(file);
                    //save excel file in your wwwroot folder and get this excel file from wwwroot
                }
            }
            //After save excel file in wwwroot and then
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    //return or alert message here
                }
                else
                {
                    //read excel file data and add data in  model.StaffInfoViewModel.StaffList
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        model.StaffInfoViewModel.StaffList.Add(new ProductOpeningBalance
                        {
                            ItemId = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim(),
                            ItemCode = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                            ItemName = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim(),
                            OpeningBalance =Convert.ToDecimal( (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim()),
                            BranchId = 1,
                            CompanyId =1,
                        });
                    }
                    foreach (var item in model.StaffInfoViewModel.StaffList)
                    {
                       
                        _db.ProductOpeningBalance.Add(item);
                        _db.SaveChangesAsync();
                    }
                   
                }
            }
            //return same view and  pass view model 
            return View(model);
        }
    }
}
