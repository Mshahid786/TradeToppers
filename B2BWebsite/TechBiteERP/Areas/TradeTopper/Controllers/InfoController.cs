using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechBiteERP.Areas.TradeTopper.Controllers
{
    [Area("TradeTopper")]
    [Authorize]
    public class InfoController : Controller
    {
        public IActionResult Counseling()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
