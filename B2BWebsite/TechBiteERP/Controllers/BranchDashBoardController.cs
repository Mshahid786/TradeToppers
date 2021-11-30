using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;

namespace TechBiteERP.Controllers
{
    public class BranchDashBoardController : Controller
    {
        private readonly ApplicationDbContext _db;
         

        public BranchDashBoardController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
     {
            if (User.Identity.IsAuthenticated)
            {
                var branch = _db.Branch.ToArray();
                for(var i=0;i<branch.Length;i++)
                {
                    TempData["Branch"+ i ] =Convert.ToString( branch[i].Photo);
                    //TempData["Branch"+ i ] =branch[i];
                }
               

                var branches = JsonConvert.SerializeObject(_db.Branch.Where(x => x.CompanyId == 1).OrderBy(x => x.ID));
                HttpContext.Session.SetString("branches", branches);
                return View("Index", "BranchDashboard");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public IActionResult AssignBranch(int BranchId)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Branch1"] = "~/TechBiteImage/Branch/avenue3.png";
                
                if (BranchId != 0)
                {
                    HttpContext.Session.SetInt32("BranchId", BranchId);
                    HttpContext.Session.SetInt32("BranchPhoto", BranchId);
                    var branchPhoto = Convert.ToString(_db.Branch.Where(x => x.ID == BranchId).FirstOrDefault().Photo);
                    HttpContext.Session.SetString("BranchPhoto", branchPhoto);
                    TempData["BranchPhoto"] = Convert.ToString( _db.Branch.Where(x => x.ID == BranchId).FirstOrDefault().Photo);
                }
                
               // return View("Index", "Dashboard");
                return RedirectToAction("Index", "PlotDashboard", new { area = "" });
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
                return View("BranchDashboard", "BranchDashboard");
            }
            else
                return RedirectToAction("Login", "Account");
        }
    }
}
