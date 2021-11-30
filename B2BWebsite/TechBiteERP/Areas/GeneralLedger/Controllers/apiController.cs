using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class apiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public apiController(ApplicationDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string getVoucherType(int id)
        {     
            return _db.VoucherTypes.Find(id).VoucherTypeDescription;

        }
    }
}
