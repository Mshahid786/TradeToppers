using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DashBoardController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var br = HttpContext.Session.GetInt32("BranchId").Value;
                    return View("Index", "Dashboard");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Login", "Account");
                }
                 
            }
            else
                return RedirectToAction("Login", "Account");
        }
        
    }
}
