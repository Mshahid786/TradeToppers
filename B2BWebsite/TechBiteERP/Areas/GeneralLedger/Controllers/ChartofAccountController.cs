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
using TechBiteERP.Model.ViewModels;

namespace TechBiteERP.Areas.GeneralLedger.Controllers
{
    [Area("GeneralLedger")]
    [Authorize]
    public class ChartofAccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly int _level1, _level2, _level3, _level4, _level5;
        private readonly string _splitter;
        Dictionary<string, object> returnResponse = new Dictionary<string, object>();
        public ChartofAccountController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _level1 = 1;
            _level2 = 2;
            _level3 = 2;
            _level4 = 4;
            _level5 = 5;
            _splitter = ".";
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.NavbarHeading = "Chart of Account";
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Branch = new SelectList(_db.Block.ToList(), "ID", "Name");
            int companyId = 1;
            ChartofAccountViewModel accountViewModel = new ChartofAccountViewModel();
            var account = _db.GLAccounts.Where(a => a.Id == id && a.IsDeleted == false && a.CompanyId == companyId).FirstOrDefault();
            if (account == null)
                return NotFound();

            var parentAccount = _db.GLAccounts.Where(a => a.Id == account.ParentId && a.IsDeleted == false && a.CompanyId == companyId).FirstOrDefault();
            ViewBag.Title = "Edit account";
            ViewBag.Action = "Edit";
            ViewBag.Level1 = _level1;
            ViewBag.Level2 = _level2;
            ViewBag.Level3 = _level3;
            ViewBag.Level4 = _level4;
            ViewBag.Level5 = _level5;
            ViewBag.Splitter = _splitter;

            accountViewModel.Id = id;
            accountViewModel.NoteNo = account.NoteNo;
            accountViewModel.NoteDescription = account.NoteDescription;
            accountViewModel.BranchId = account.BranchId;
            accountViewModel.Name = account.Name;
            accountViewModel.Active = (account.IsActive ? "on" : "");
            accountViewModel.RequireCostCenter = (account.RequireCostCenter ? "on" : "");
            accountViewModel.RequireSubAccount = (account.RequireSubAccount ? "on" : "");
            accountViewModel.AccountLevel = account.AccountLevel;
            accountViewModel.SubAccountId = new string[] { "1", "3" };// JsonConvert.DeserializeObject(account.SubAccountId);
            accountViewModel.SubAccount = account.SubAccountId;
            if (parentAccount != null)
            {
                accountViewModel.ParentCode = parentAccount.Code;
                accountViewModel.Code = account.Code.Substring(parentAccount.Code.Length, account.Code.Length - parentAccount.Code.Length);
            }
            else
                accountViewModel.Code = account.Code;

            //Get Sub accounts 
            if (accountViewModel.AccountLevel == 4)
            {
                IEnumerable<GLSAccount> subAccount = _db.GLSAccounts.Where(s => s.IsDeleted == false);
                accountViewModel.SubAccountList = subAccount;
            }
            return View("CreateAccount", accountViewModel);
        }
        public IActionResult CreateSibling(int id)
        {
            ViewBag.Branch = new SelectList(_db.Block.ToList(), "ID", "Name");
            int companyId = 1;
            ChartofAccountViewModel accountViewModel = new ChartofAccountViewModel();
            var parentAccount = _db.GLAccounts.Where(a => a.Id == id && a.IsDeleted == false && a.CompanyId == companyId).FirstOrDefault(); //get parentId
            if (parentAccount == null)
                return NotFound("Parent account not found, please refresh page");

            //get last account code
            var account = _db.GLAccounts
                        .Where(a => a.ParentId == id && a.CompanyId == companyId && a.IsDeleted == false)
                        .OrderByDescending(a => a.Code)
                        .FirstOrDefault();

            string newCode;
            int newCodeLength;
            short newAccountLevel = (short)(parentAccount.AccountLevel + 1);
            if (newAccountLevel == 1)
                newCodeLength = _level1;
            else if (newAccountLevel == 2)
                newCodeLength = _level2;
            else if (newAccountLevel == 3)
                newCodeLength = _level3;
            else if (newAccountLevel == 4)
                newCodeLength = _level4;
            else
                newCodeLength = _level5;
            if (account == null) //means no child row, start with 1
            {
                newCode = "1";
                newCode = newCode.PadLeft(newCodeLength, '0');
            }
            else  //increment 1 into last code
            {
                newCode = account.Code.Substring(account.Code.Length - newCodeLength, newCodeLength);
                int c = Convert.ToInt16(newCode) + 1;
                newCode = c.ToString();
                newCode = newCode.PadLeft(newCodeLength, '0');
            }
            //var account = _db.GLAccounts.Where(a => a.Id == account.ParentId && a.IsDeleted == false).FirstOrDefault();
            ViewBag.Title = "Create account";
            ViewBag.Action = "Create";
            ViewBag.Level1 = _level1;
            ViewBag.Level2 = _level2;
            ViewBag.Level3 = _level3;
            ViewBag.Level4 = _level4;
            ViewBag.Level5 = _level5;
            ViewBag.Splitter = _splitter;

            accountViewModel.ParentId = id;
            accountViewModel.AccountLevel = newAccountLevel;
            accountViewModel.ParentCode = parentAccount.Code;
            accountViewModel.Code = newCode;
            accountViewModel.NewCodeLength = newCodeLength;
            accountViewModel.Name = "";

            //Get Sub accounts 
            if (newAccountLevel == 4)
            {
                IEnumerable<GLSAccount> subAccount = _db.GLSAccounts.Where(s => s.IsDeleted == false);
                accountViewModel.SubAccountList = subAccount;
            }
            return View("CreateAccount", accountViewModel);
        }
        public IActionResult CreateChild(int id)
        {
            ViewBag.Branch = new SelectList(_db.Block.ToList(), "ID", "Name");
            int companyId = 1;
            ChartofAccountViewModel accountViewModel = new ChartofAccountViewModel();

            //get parent account code
            var parentAccount = _db.GLAccounts.Where(a => a.Id == id && a.CompanyId == companyId && a.IsDeleted == false).FirstOrDefault();
            if (parentAccount == null)
                return NotFound();

            //get last account code
            var account = _db.GLAccounts
                        .Where(a => a.ParentId == id && a.CompanyId == companyId && a.IsDeleted == false)
                        .OrderByDescending(a => a.Code)
                        .FirstOrDefault();

            string newCode;
            int newCodeLength;
            short newAccountLevel = (short)(parentAccount.AccountLevel + 1);
            if (newAccountLevel == 1)
                newCodeLength = _level1;
            else if (newAccountLevel == 2)
                newCodeLength = _level2;
            else if (newAccountLevel == 3)
                newCodeLength = _level3;
            else if (newAccountLevel == 4)
                newCodeLength = _level4;
            else
                newCodeLength = _level5;
            if (account == null) //means no child row, start with 1
            {
                newCode = "1";
                newCode = newCode.PadLeft(newCodeLength, '0');
            }
            else  //increment 1 into last code
            {
                newCode = account.Code.Substring(account.Code.Length - newCodeLength, newCodeLength);
                int c = Convert.ToInt16(newCode) + 1;
                newCode = c.ToString();
                newCode = newCode.PadLeft(newCodeLength, '0');
            }
            //var account = _db.GLAccounts.Where(a => a.Id == account.ParentId && a.IsDeleted == false).FirstOrDefault();
            ViewBag.Title = "Create account";
            ViewBag.Action = "Create";
            ViewBag.Level1 = _level1;
            ViewBag.Level2 = _level2;
            ViewBag.Level3 = _level3;
            ViewBag.Level4 = _level4;
            ViewBag.Level5 = _level5;
            ViewBag.Splitter = _splitter;

            accountViewModel.ParentId = id;
            accountViewModel.AccountLevel = newAccountLevel;
            accountViewModel.ParentCode = parentAccount.Code;
            accountViewModel.Code = newCode;
            accountViewModel.NewCodeLength = newCodeLength;
            accountViewModel.Name = "";

            //Get Sub accounts 
            if (newAccountLevel == 5)
            {
                IEnumerable<GLSAccount> subAccount = _db.GLSAccounts.Where(s => s.IsDeleted == false);
                accountViewModel.SubAccountList = subAccount;
            }
            return View("CreateAccount", accountViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Post(int id, ChartofAccountViewModel accountModel)
        {

            try
            {
                int companyId = 1;
               // string userId = HttpContext.Session.GetString("UserId");
                if (accountModel.Action == "Create")
                {
                    string newCode = string.Concat(accountModel.ParentCode, accountModel.Code);
                    var accountDuplicate = _db.GLAccounts.Where(a => a.Code == newCode && a.CompanyId == companyId && a.IsDeleted == false);
                    if (accountDuplicate.Count() != 0)
                    {
                        returnResponse.Add("success", false);
                        returnResponse.Add("message", string.Format("Account with code [{0}] already exist, please enter new code.", newCode));
                        return Ok(returnResponse);
                    }
                    GLAccount account = new GLAccount();
                    account.AccountLevel = accountModel.AccountLevel;
                    account.Code = newCode;
                    account.ParentId = accountModel.ParentId;
                    account.CompanyId = companyId;
                    account.CreatedBy = "";
                    account.CreatedDate = DateTime.Now;
                    if (account.AccountLevel == 5)
                    { //only 4 level account have property of active/in-active
                        account.IsActive = (accountModel.Active == "on" ? true : false);
                        account.RequireCostCenter = (accountModel.RequireCostCenter == "on" ? true : false);
                        account.RequireSubAccount = (accountModel.RequireSubAccount == "on" ? true : false);
                        account.SubAccountId = JsonConvert.SerializeObject(accountModel.SubAccountId);
                    }
                    else
                        account.IsActive = true;
                    account.NoteNo = accountModel.NoteNo;
                    account.NoteDescription = accountModel.NoteDescription;
                    account.BranchId = accountModel.BranchId;
                    account.Name = accountModel.Name;
                    account.IsDeleted = false;
                    _db.GLAccounts.Add(account);
                    await _db.SaveChangesAsync();
                    returnResponse.Add("success", true);
                    returnResponse.Add("message", string.Concat("Account has been created <br>", account.Code, " - ", account.Name));
                    return Ok(returnResponse);
                }
                else if (accountModel.Action == "Edit")
                {
                    GLAccount account = new GLAccount();
                    account = _db.GLAccounts.Find(accountModel.Id);
                    account.Name = accountModel.Name;
                    account.NoteNo = accountModel.NoteNo;
                    account.NoteDescription = accountModel.NoteDescription;
                    account.BranchId = accountModel.BranchId;
                    if (accountModel.AccountLevel == 5)
                    {
                        account.IsActive = (accountModel.Active == "on" ? true : false);
                        account.RequireCostCenter = (accountModel.RequireCostCenter == "on" ? true : false);
                        account.RequireSubAccount = (accountModel.RequireSubAccount == "on" ? true : false);
                        account.SubAccountId = JsonConvert.SerializeObject(accountModel.SubAccountId);
                    }


                    var entry = _db.GLAccounts.Update(account);
                    entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());
                    await _db.SaveChangesAsync();
                    returnResponse.Add("success", true);
                    returnResponse.Add("message", string.Concat("Account has been updated <br>", account.Code, " - ", account.Name));
                    return Ok(returnResponse);
                }
                else
                {
                    var parentAccount = _db.GLAccounts.Find(id);
                    string newCode = string.Concat(parentAccount.Code, _splitter, accountModel.Code);

                    //check for duplication
                    var accountDuplicate = _db.GLAccounts.Where(a => a.Code == newCode && a.IsDeleted == false);
                    if (accountDuplicate != null)
                    {
                        throw new Exception(string.Format("Account with code [{0}] already exist, please enter new code.", newCode));
                    }
                    else
                    {

                    }

                }
                return CreatedAtAction(nameof(Post), 1);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public IActionResult AccountCreated()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<AccountTree> GetChartofAccount()
        {
            int companyId = 1;
            var accounts = _db.GLAccounts.Where(a => a.IsDeleted == false && a.CompanyId == companyId);
            List<AccountTree> trees = new List<AccountTree>();
            foreach (var account in accounts)
            {
                AccountTree tree = new AccountTree();
                tree.Id = account.Id;
                tree.Parent = (account.ParentId == 0 ? "#" : account.ParentId.ToString());
                tree.Text = string.Concat(account.Code, " - ", account.Name);
                if (!account.IsActive)
                    tree.Icon = "far fa-file-minus";
                else if (account.AccountLevel == 4)
                    tree.Icon = "far fa-file";
                else
                    tree.Icon = "far fa-folder";
                trees.Add(tree);
            }
            return trees;
        }
        public IActionResult GetAllAccounts(int? accountLevel = 4)
        {
            var accounts = _db.GLAccounts.Where(
                                                a => a.IsDeleted == false && a.Company.Id == HttpContext.Session.GetInt32("CompanyId").Value && a.AccountLevel == accountLevel
                                               )
                                               .Select(a => new
                                               {
                                                   Id = a.Id,
                                                   Name = string.Concat(a.Code, " - ", a.Name)
                                               })
                                               .OrderBy(a => a.Name)
                                               .ToList();

            return Ok(accounts);
        }


        [HttpGet]
        public IActionResult GetAccounts(string q = "")
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            //if (q == "")
            //{
            //    return Ok(new
            //    {
            //        message = "Validation Required",
            //        error = "Missing query {q}"

            //    });
            //}
            var accounts = _db.GLAccounts.Where(
                                                a => a.IsDeleted == false && a.Company.Id == companyId && a.AccountLevel == 4
                                                && (a.Code.Contains(q) || a.Name.Contains(q)
                                               ))
                                               .Select(a => new
                                               {
                                                   id = a.Id,
                                                   text = string.Concat(a.Code, " - ", a.Name),
                                                   code = a.Code,
                                                   name = a.Name
                                               })
                                               .OrderBy(a => a.text)
                                               .Take(25)
                                               .ToList();
            return Ok(accounts);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            ViewBag.Branch = new SelectList(_db.Block.ToList(), "ID", "Name");
            int companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var account = _db.GLAccounts.Where(a => a.IsDeleted == false && a.Company.Id == companyId && a.Id == id)
                                               .Select(a => new
                                               {
                                                   id = a.Id,
                                                   text = string.Concat(a.Code, " - ", a.Name)
                                               })
                                               .FirstOrDefault();
            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //check if account is already in use
                var accountUsedInVoucher = _db.JOURNALDT.Where(v => v.ACNO == id).FirstOrDefault();
                var accountUsedInItemAccounts = _db.Items.Where(i => i.StockGLAccountId == id || i.MaterialGLAccountId == id).FirstOrDefault();
                var basicSetting = _db.BasicSettings.Where(c => c.AdjustedItemAccountId == id).FirstOrDefault();
                if (accountUsedInVoucher == null && accountUsedInItemAccounts == null && basicSetting == null )
                {
                    string userId = HttpContext.Session.GetString("UserId");

                    var account = _db.GLAccounts.Where(a => a.Id == id).FirstOrDefault();
                    account.IsDeleted = true;
                    account.UpdatedBy = userId;
                    account.UpdatedDate = DateTime.Now;
                    var entry = _db.GLAccounts.Update(account);
                    entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());
                    await _db.SaveChangesAsync();
                    returnResponse.Add("success", true);
                    returnResponse.Add("message", "Account has been deleted!");
                    return Ok(returnResponse);
                }
                else
                {
                    //return NotFound(string.Format("Cannot delete account because it is used in Voucher#: {0} and Bank Cash: {1}",accountUsedInVoucher.VoucherId,accountUsedInBankCash.AccountName));
                    return NotFound("This Account is in Use and cannot be Deleted.");
                }
            }
            catch (Exception exc)
            {
                return NotFound(exc.Message == null ? exc.InnerException.Message.ToString() : exc.Message.ToString());
            }
        }

        [HttpGet]
        public IActionResult GetCashBankAccounts(string q = "")
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId").Value;
            var accounts = _db.GLAccounts.Where(
                                                a => a.IsDeleted == false && a.Company.Id == companyId && a.AccountLevel == 4
                                                && (a.Code.Contains(q) || a.Name.Contains(q)
                                               ))
                                               .Select(a => new
                                               {
                                                   id = a.Id,
                                                   text = string.Concat(a.Code, " - ", a.Name)
                                               })
                                               .OrderBy(a => a.text)
                                               .Take(25)
                                               .ToList();
            return Ok(accounts);
        }
    }
    public class AccountTree
    {
        public int Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
    }
}
