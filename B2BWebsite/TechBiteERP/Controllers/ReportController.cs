using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;
using TechBiteERP.ReportProject.Reports;

namespace TechBiteERP.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly ApplicationDbContext _db;
        //rptRepository _rptRepo = new rptRepository();
        private readonly IConfiguration _Configuration;
        
        public ReportController(IWebHostEnvironment webHostEnv,ApplicationDbContext db,IConfiguration configuration)
        {
           this._webHostEnv = webHostEnv;
            _db = db;
            _Configuration = configuration;
           
           System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            TempData["ReportName"] = "Reports";
            ViewBag.Reports = new SelectList(_db.ApplicationReports.Where(x => x.Type != "Account").ToList(), "ID", "Name").ToList();
            ViewBag.AccountNote = new SelectList(_db.GLAccounts.ToList(), "NoteNo", "NoteNo").Distinct().ToList();
            ViewBag.Branch = new SelectList(_db.Branch.ToList(), "ID", "POSName").ToList();
            ViewBag.Region = new SelectList(_db.Regions.ToList(), "RegionID", "RegionName").ToList();
            ViewBag.City = new SelectList(_db.Cities.ToList(), "CityID", "CityName").ToList();
            ViewBag.Area = new SelectList(_db.Areas.ToList(), "AreaID", "AreaName").ToList();
            ViewBag.SaleType = new SelectList(_db.ApplicationReports.ToList(), "ID", "Name").ToList();
            ViewBag.Accounts = new SelectList(_db.GLAccounts.ToList(), "Id", "Name").ToList();
            ViewBag.Customer = new SelectList(_db.ArCustomer.ToList(), "Id", "Name").ToList();
            ViewBag.Supplier = new SelectList(_db.Suppliers.ToList(), "Id", "Name").ToList();
            ViewBag.SalesMan = new SelectList(_db.Salesman.ToList(), "Id", "Name").ToList();
            ViewBag.ReportType = new SelectList(_db.ApplicationReports.Where(x=>x.Type != "Account").ToList(), "ID", "Name").ToList();
            return View();
        }
        public IActionResult Ledger()
        {
            TempData["ReportName"] = "General Ledger ";
            ViewBag.Reports = new SelectList(_db.ApplicationReports.Where(x => x.Type == "Account").ToList(), "ID", "Name").ToList();
            ViewBag.Branch = new SelectList(_db.Branch.ToList(), "ID", "POSName").ToList();
            ViewBag.Accounts = new SelectList(_db.GLAccounts.ToList(), "Id","Name").ToList();
            // ViewBag.CostCenter = new SelectList(_db.CostCenter.ToList(), "CityID", "CityName").ToList();
            ViewBag.ReportType = new SelectList(_db.ApplicationReports.Where(x => x.Type == "Account").ToList(), "ID", "Name").ToList();
            return View();
        }

        public IActionResult DailySaleReport()
        {
            TempData["ReportName"] = "Daily Sale Report ";
            ViewBag.Reports = new SelectList(_db.ApplicationReports.Where(x => x.Type == "Account").ToList(), "ID", "Name").ToList();
            ViewBag.Branch = new SelectList(_db.Branch.ToList(), "ID", "POSName").ToList();
            ViewBag.Customers = new SelectList(_db.ArCustomer.ToList(), "Id", "Name");
            // ViewBag.CostCenter = new SelectList(_db.CostCenter.ToList(), "CityID", "CityName").ToList();
            ViewBag.ReportType = new SelectList(_db.ApplicationReports.Where(x => x.Type == "Account").ToList(), "ID", "Name").ToList();
            return View();
        }
        public IActionResult Print(string reportParameters,string reportName)
        {
           
            var prms = TempData["Parameters"];
            var rptRepo = new GLRepository(_Configuration);
            List<String> Items = reportParameters.Split(",").Select(i => i.Trim()).Where(i => i != string.Empty).ToList(); //Split them all and remove spaces
            //Items.Remove("v"); //or whichever you want
            string NewX = String.Join(", ", Items.ToArray());

            reportName = GetParameterValue("ReportTitle", reportParameters);
            reportName = _db.ApplicationReports.Where(x => x.ID == Convert.ToInt32(reportName)).FirstOrDefault().Name;
            DataTable dt = new DataTable();
            int companyId = 1;
            string CompanyName = _db.ApplicationCompanies.Where(x => x.Id == 1).FirstOrDefault().Name;
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnv.WebRootPath}\\Reports\\ChartOfAccount.rdlc";
            Dictionary<string, string> Parameters = new Dictionary<string, string>();
            if (reportName=="Chart Of Account")
            {
                
                int accountlevel =Convert.ToInt32( GetParameterValue("AccountLevel", reportParameters));
                dt = rptRepo.GetChartOfAccount(companyId, accountlevel);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\ChartOfAccount.rdlc";
                Parameters.Add("ReportName", "Chart Of Account"); 
            }
            else if (reportName== "Note Of Account")
            {
                
                int accountlevel =Convert.ToInt32( GetParameterValue("AccountNote", reportParameters));
                int branchId = Convert.ToInt32(GetParameterValue("Branch", reportParameters));
                DateTime fromDate = Convert.ToDateTime(GetParameterValue("CreatedDate", reportParameters));
                DateTime toDate = Convert.ToDateTime(GetParameterValue("UpdatedDate", reportParameters));
                dt = rptRepo.GetNoteOfAccount(companyId, accountlevel, fromDate, toDate, branchId);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\NoteOfAccount.rdlc";
                Parameters.Add("ReportName", "Note Of Account"); 
            }
            else if (reportName == "Trial Balance")
            {

                int accountlevel = Convert.ToInt32(GetParameterValue("AccountLevel", reportParameters));
                DateTime fromDate = Convert.ToDateTime(GetParameterValue("CreatedDate", reportParameters));
                DateTime toDate = Convert.ToDateTime(GetParameterValue("UpdatedDate", reportParameters));
                int branchId = Convert.ToInt32(GetParameterValue("Branch", reportParameters));
                dt = rptRepo.GetTrialBalance(companyId,accountlevel, fromDate,toDate, branchId);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\TrialBalance.rdlc";
                Parameters.Add("ReportName", "Trial Balance");
            }
            else if (reportName == "Balance Sheet")
            {

                DateTime toDate = Convert.ToDateTime(GetParameterValue("UpdatedDate", reportParameters));
                int branchId = Convert.ToInt32(GetParameterValue("Branch", reportParameters));
                dt = rptRepo.GetBalanceSheet(companyId, toDate, branchId);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\BalanceSheet.rdlc";
                Parameters.Add("ReportName", "Balance Sheet");
            }
            else if (reportName == "Country")
            {

                dt = rptRepo.GetCountry(companyId, "Countries");
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\CommonReport.rdlc";
                Parameters.Add("ReportName", "Country");
            }
            else if (reportName == "City")
            {

                dt = rptRepo.GetCity(companyId, "Cities");
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\CommonReport.rdlc";
                Parameters.Add("ReportName", "City");
            }
            else if (reportName == "District")
            {

                dt = rptRepo.GetDistrict(companyId, "Districts");
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\CommonReport.rdlc";
                Parameters.Add("ReportName", "District");
            }
            else if (reportName == "Area")
            {

                dt = rptRepo.GetArea(companyId, "Areas");
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\CommonReport.rdlc";
                Parameters.Add("ReportName", "Area");
            }
            else if (reportName == "Province")
            {
                dt = rptRepo.GetProvince(companyId, "Provinces");
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\CommonReport.rdlc";
                Parameters.Add("ReportName", "Province");
            }
            else if (reportName == "Ledger")
            {
                DateTime fromDate = Convert.ToDateTime(GetParameterValue("CreatedDate", reportParameters));
                DateTime toDate = Convert.ToDateTime(GetParameterValue("UpdatedDate", reportParameters));
                int Posted = Convert.ToInt32(GetParameterValue("vtype", reportParameters));
                int FromAccNo = Convert.ToInt32(GetParameterValue("fromAccount", reportParameters));
                int ToAccNo = Convert.ToInt32(GetParameterValue("toAccount", reportParameters));
                int branchId = Convert.ToInt32(GetParameterValue("Branch", reportParameters));
                string allCheck = Convert.ToString(GetParameterValue("allCheck", reportParameters));
                if(allCheck=="on")
                {
                    FromAccNo = 0;
                    ToAccNo =( from a in _db.GLAccounts orderby  a.Id descending select a.Id).FirstOrDefault();
                }
                dt = rptRepo.GetLedger(branchId,companyId, Posted,fromDate,toDate,FromAccNo, ToAccNo);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\GeneralLedger.rdlc";
                Parameters.Add("ReportName", "General Ledger");
            }
            else if (reportName == "Sale Report")
            {

                int accountlevel = Convert.ToInt32(GetParameterValue("AccountLevel", reportParameters));
                dt = rptRepo.GetPlotSale(companyId, accountlevel);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotSaleReport.rdlc";
                Parameters.Add("ReportName", "Plot Sale Report");
            }
            else if (reportName == "Plot Recovery")
            {
                DateTime fromDate = Convert.ToDateTime(GetParameterValue("CreatedDate", reportParameters));
                DateTime toDate = Convert.ToDateTime(GetParameterValue("UpdatedDate", reportParameters));

                dt = rptRepo.GetPlotRecovery(companyId, fromDate, toDate);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotRecovery.rdlc";
                Parameters.Add("ReportName", "Plot Recovery Report");
            }
            else if (reportName == "Registeration")
            {

                int id = Convert.ToInt32(GetParameterValue("Id", reportParameters));
                dt = rptRepo.GetSaleRegisteration(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\Registeration.rdlc";
                Parameters.Add("ReportName", "Registeration");
            }
            else if (reportName == "PaymentHistory")
            {

                int id = Convert.ToInt32(GetParameterValue("Id", reportParameters));
                dt = rptRepo.GetPaymentHistory(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PaymentHistory.rdlc";
                Parameters.Add("ReportName", "Payment History");
            }
            else if (reportName == "Item")
            {
                dt = rptRepo.GetItem(companyId,0);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\Item.rdlc";
                Parameters.Add("ReportName", "Item Report");
            }
            Parameters.Add("CompanyName", CompanyName);            
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("TechBiteDataSet",dt);
            var result = localReport.Execute(RenderType.Pdf, extension, Parameters, mimtype);
            return File(result.MainStream, "application/pdf");
            
        }

        public IActionResult PrintBaseReport(int id, string reportName)
        {
            var prms = TempData["Parameters"];
            var rptRepo = new GLRepository(_Configuration);
            DataTable dt = new DataTable();
            int companyId = 1;
            string CompanyName = _db.ApplicationCompanies.Where(x => x.Id == 1).FirstOrDefault().Name;
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnv.WebRootPath}\\Reports\\ChartOfAccount.rdlc";
            Dictionary<string, string> Parameters = new Dictionary<string, string>();
             if (reportName == "Registeration")
            {

                dt = rptRepo.GetSaleRegisteration(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\Registeration.rdlc";
                Parameters.Add("ReportName", "Registeration");
            }
            else if (reportName == "PaymentHistory")
            {
  
                dt = rptRepo.GetPaymentHistory(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PaymentHistory.rdlc";
                Parameters.Add("ReportName", "Payment History");
            } 
            else if (reportName == "BookingCertificate")
            {
  
                dt = rptRepo.GetBokingCertificate(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\BookingCertificate.rdlc";
                Parameters.Add("ReportName", "Booking Certificate");
            }
            else if (reportName == "PlotTransfer")
            {

                dt = rptRepo.GetPlotTransfer(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\TransferReport.rdlc";
                Parameters.Add("ReportName", "Plot Transfer");
            }
            else if (reportName == "PlotFile")
            {

                dt = rptRepo.GetPlotFile(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotFile.rdlc";
                Parameters.Add("ReportName", "Plot file");
            }
            else if (reportName == "PlotSale")
            {

                dt = rptRepo.GetPlotSaleBase(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotSaleReport.rdlc";
                Parameters.Add("ReportName", "Plot Sale");
            }
            else if (reportName == "PaymentPlan")
            {

                dt = rptRepo.GetPaymentPlan(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PaymentPlan.rdlc";
                Parameters.Add("ReportName", "Payment Plan");
            }
            else if (reportName == "RegisterationForms")
            {

                dt = rptRepo.GetRegisterationforms(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PaymentPlan.rdlc";
                Parameters.Add("ReportName", "Payment Plan");
            }
            else if (reportName == "PlotCancellation")
            {

                dt = rptRepo.GetPlotCancellation(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotCancellation.rdlc";
                Parameters.Add("ReportName", "Plot Cancellation");
            }
            else if (reportName == "PlotRecovery")
            {
                dt = rptRepo.GetPlotRecoveryBase(companyId, id);
                path = $"{this._webHostEnv.WebRootPath}\\Reports\\PlotRecovery.rdlc";
                Parameters.Add("ReportName", "Plot Recovery Report");
            }
            Parameters.Add("CompanyName", CompanyName);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("TechBiteDataSet", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, Parameters, mimtype);
            return File(result.MainStream, "application/pdf");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Dictionary<string, string> parameters, string[] Items, string[] Categories,
           string[] Manufacturers, string[] Customers,/*string[] Cities,*/string[] Suppliers, string[] SalesPerson, string[] CustomerCategory, string[] Brandes, string[] Sizes)
        {
            try
            {
                string reportName = Convert.ToString(parameters["ReportTitle"]);
                TempData["Parameters"] = parameters;
                //var report = _dbContext.AppReports.Where(n => n.Name == reportName).FirstOrDefault();

                var reportPath = "/Report/Print";
                var reportUrl = string.Concat(reportPath, "?reportParameters=", JsonConvert.SerializeObject(parameters));
                return Ok(reportUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetParameterValue(string parameterName,string parameters)
        {
            dynamic  jsonObj = JsonConvert.DeserializeObject(parameters);
            foreach (var item in jsonObj)
            {
                if (item.Name == parameterName)
                {
                    return item.Value;
                }
            }
            return "";
        }

    }
}
