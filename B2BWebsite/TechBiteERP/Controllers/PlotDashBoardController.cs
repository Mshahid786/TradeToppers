using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;

namespace TechBiteERP.Controllers
{
    public class PlotDashBoardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlotDashBoardController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index(string branch ="")
     {
            if (User.Identity.IsAuthenticated)
            {
                if (branch == "ALL")
                {
                    var branchId = HttpContext.Session.GetInt32("BranchId").Value;
                    var TotalPlot = _db.Items.Count();
                    var TotalSold = _db.PlotSale.Where(x => x.PlotStatusId == 3 ).Count();
                    var TotalAvailable = _db.PlotSale.Where(x => x.PlotStatusId == 4  ).Count();
                    var TotalReserved = _db.PlotSale.Where(x => x.PlotStatusId == 1 ).Count();
                    var TotalRegistered = _db.PlotRegisterationForm.Where(x =>  x.Canceled == false).Count();
                    var TotalConfirmed = _db.PlotSale.Where(x => x.PlotStatusId == 6 ).Count();
                    var TotalInstallment = _db.PlotSale.Where(x => x.PlotStatusId == 8 ).Count();
                    var TotalPossession = _db.PlotSale.Where(x => x.PlotStatusId == 7 ).Count();
                    var TotalAllotment = _db.PlotSale.Where(x => x.PlotStatusId == 9 ).Count();
                    var TotalBooked = _db.PlotSale.Where(x => x.PlotStatusId == 2 ).Count();

                    ViewBag.TotalPlot = TotalPlot;

                    ViewBag.TotalSold = TotalSold;
                    ViewBag.TotalAvailable = TotalAvailable;
                    ViewBag.TotalReserved = TotalReserved;
                    ViewBag.TotalRegistered = TotalRegistered;
                    ViewBag.TotalConfirmed = TotalConfirmed;
                    ViewBag.TotalInstallment = TotalInstallment;
                    ViewBag.TotalPossession = TotalPossession;
                    ViewBag.TotalAllotment = TotalAllotment;
                    ViewBag.TotalBooked = TotalBooked;
                    if (TotalPlot != 0)
                    {


                        ViewBag.TotalSoldPer =Math.Round( Convert.ToDecimal(TotalSold / TotalPlot) * 100);
                        ViewBag.TotalAvailablePer = Math.Round(Convert.ToDecimal(TotalAvailable) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalReservedPer = Math.Round(Convert.ToDecimal(TotalReserved) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalRegisteredPer = Math.Round(Convert.ToDecimal(TotalRegistered) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalConfirmedPer = Math.Round(Convert.ToDecimal(TotalConfirmed) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalInstallmentPer = Math.Round(Convert.ToDecimal(TotalInstallment) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalPossessionPer = Math.Round(Convert.ToDecimal(TotalPossession) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalAllotmentPer = Math.Round(Convert.ToDecimal(TotalAllotment) / Convert.ToDecimal(TotalPlot) * 100);
                        ViewBag.TotalBookedPer = Math.Round(Convert.ToDecimal(TotalBooked) / Convert.ToDecimal(TotalPlot) * 100);
                        // ViewBag.TotalPlot = _db.Items.Count();
                    }
                    else
                    {
                        ViewBag.TotalSoldPer = 0;
                        ViewBag.TotalAvailablePer = 0;
                        ViewBag.TotalReservedPer = 0;
                        ViewBag.TotalRegisteredPer = 0;
                        ViewBag.TotalConfirmedPer = 0;
                        ViewBag.TotalInstallmentPer = 0;
                        ViewBag.TotalPossessionPer = 0;
                        ViewBag.TotalAllotmentPer = 0;
                        ViewBag.TotalBookedPer = 0;
                    }
                }
                else
                {
                    var branchId = HttpContext.Session.GetInt32("BranchId").Value;
                    var TotalPlot = _db.Items.Where(x => x.BrandID == branchId).Count();
                    var TotalSold = _db.PlotSale.Where(x => x.PlotStatusId == 3 && x.BranchId == branchId).Count();
                    var TotalAvailable = _db.PlotSale.Where(x => x.PlotStatusId == 4 && x.BranchId == branchId).Count();
                    var TotalReserved = _db.PlotSale.Where(x => x.PlotStatusId == 1 && x.BranchId == branchId).Count();
                    var TotalRegistered = _db.PlotRegisterationForm.Where(x => x.BranchId == branchId && x.Canceled == false).Count();
                    var TotalConfirmed = _db.PlotSale.Where(x => x.PlotStatusId == 6 && x.BranchId == branchId).Count();
                    var TotalInstallment = _db.PlotSale.Where(x => x.PlotStatusId == 8 && x.BranchId == branchId).Count();
                    var TotalPossession = _db.PlotSale.Where(x => x.PlotStatusId == 7 && x.BranchId == branchId).Count();
                    var TotalAllotment = _db.PlotSale.Where(x => x.PlotStatusId == 9 && x.BranchId == branchId).Count();
                    var TotalBooked = _db.PlotSale.Where(x => x.PlotStatusId == 2 && x.BranchId == branchId).Count();

                    ViewBag.TotalPlot = TotalPlot;

                    ViewBag.TotalSold = TotalSold;
                    ViewBag.TotalAvailable = TotalAvailable;
                    ViewBag.TotalReserved = TotalReserved;
                    ViewBag.TotalRegistered = TotalRegistered;
                    ViewBag.TotalConfirmed = TotalConfirmed;
                    ViewBag.TotalInstallment = TotalInstallment;
                    ViewBag.TotalPossession = TotalPossession;
                    ViewBag.TotalAllotment = TotalAllotment;
                    ViewBag.TotalBooked = TotalBooked;
                    if (TotalPlot != 0)
                    {


                        ViewBag.TotalSoldPer = Convert.ToDecimal(TotalSold / TotalPlot) * 100;
                        ViewBag.TotalAvailablePer = Convert.ToDecimal(TotalAvailable) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalReservedPer = Convert.ToDecimal(TotalReserved) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalRegisteredPer = Convert.ToDecimal(TotalRegistered) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalConfirmedPer = Convert.ToDecimal(TotalConfirmed) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalInstallmentPer = Convert.ToDecimal(TotalInstallment) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalPossessionPer = Convert.ToDecimal(TotalPossession) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalAllotmentPer = Convert.ToDecimal(TotalAllotment) / Convert.ToDecimal(TotalPlot) * 100;
                        ViewBag.TotalBookedPer = Convert.ToDecimal(TotalBooked) / Convert.ToDecimal(TotalPlot) * 100;
                        // ViewBag.TotalPlot = _db.Items.Count();
                    }
                    else
                    {
                        ViewBag.TotalSoldPer = 0;
                        ViewBag.TotalAvailablePer = 0;
                        ViewBag.TotalReservedPer = 0;
                        ViewBag.TotalRegisteredPer = 0;
                        ViewBag.TotalConfirmedPer = 0;
                        ViewBag.TotalInstallmentPer = 0;
                        ViewBag.TotalPossessionPer = 0;
                        ViewBag.TotalAllotmentPer = 0;
                        ViewBag.TotalBookedPer = 0;
                    }
                    var FinancialYear = HttpContext.Session.GetInt32("FinancialYearId").Value;
                }
                return View("Index", "Dashboard");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public IActionResult BranchDashboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.TotalPlot = _db.Items.Count();
                ViewBag.TotalSold = _db.PlotSale.Where(x => x.PlotStatusId == 3).Count();
                ViewBag.TotalAvailable = _db.PlotSale.Where(x => x.PlotStatusId == 4).Count();
                ViewBag.TotalReserved = _db.PlotSale.Where(x => x.PlotStatusId == 1).Count();
                ViewBag.TotalRegistered = _db.PlotSale.Where(x => x.PlotStatusId == 5).Count();
                ViewBag.TotalConfirmed = _db.PlotSale.Where(x => x.PlotStatusId == 6).Count();
                ViewBag.TotalInstallment = _db.PlotSale.Where(x => x.PlotStatusId == 8).Count();
                ViewBag.TotalPossession = _db.PlotSale.Where(x => x.PlotStatusId == 7).Count();
                ViewBag.TotalAllotment = _db.PlotSale.Where(x => x.PlotStatusId == 9).Count();
                ViewBag.TotalBooked = _db.PlotSale.Where(x => x.PlotStatusId == 2).Count();
                // ViewBag.TotalPlot = _db.Items.Count();
                return View("BranchDashboard", "PlotDashboard");
            }
            else
                return RedirectToAction("Login", "Account");
        }
    }
}
