using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;
        private readonly IHttpContextAccessor IHttpContext;
        public string ConnectionString { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _options = options;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            IHttpContext = httpContext;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Used whene we create new object of dbContext

        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString).EnableSensitiveDataLogging();
            }
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder()
        //                            .SetBasePath(Directory.GetCurrentDirectory())
        //                            .AddJsonFile("appsettings.json");
        //    var configuration = builder.Build();
        //    optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ApplicationCompany> ApplicationCompanies { get; set; }
        public DbSet<AppValueBase> AppValueBases { get; set; }
        public DbSet<AppValue> AppValues { get; set; }
        public DbSet<ApplicationRoleMenu> ApplicationRoleMenus { get; set; }
        public DbSet<GLAccount> GLAccounts { get; set; }
        public DbSet<GLSAccount> GLSAccounts { get; set; }
        public DbSet<Salesman> Salesman { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<HREmployee> HREmployees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ItemClass> ItemClasses { get; set; }
        public DbSet<PriceCategory> PriceCategories { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Uom> Uoms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Packing> Packings { get; set; }
        public DbSet<MasterItem> MasterItems { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<BasicSetting> BasicSettings { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<OpeningBalance> OpeningBalance { get; set; }
        public DbSet<FinYear> FinYear { get; set; }
        public DbSet<JOURNAL> JOURNAL { get; set; }
        public DbSet<JOURNALDT> JOURNALDT { get; set; }
        public DbSet<TechBiteERP.Model.Models.Area> Areas { get; set; }

        public DbSet<PlotSale> PlotSale { get; set; }
        public DbSet<PlotStatus> PlotStatus { get; set; }
        public DbSet<ArCustomer> ArCustomer { get; set; }
        public DbSet<PriceList> PriceList { get; set; }
        public DbSet<PlotFile> PlotFile { get; set; }
        public DbSet<PlotRecovery> PlotRecovery { get; set; }
        public DbSet<DeliveredVia> DeliveredVia { get; set; }
        public DbSet<BranchType> BranchType { get; set; }
        public DbSet<ActionType> ActionType { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Block> Block { get; set; }

        public DbSet<DealValidity> DealValidity { get; set; }
        public DbSet<DiscountFactor> DiscountFactor { get; set; }
        public DbSet<PlotRegisterationForm> PlotRegisterationForm { get; set; }
        public DbSet<PlotTransferHistory> PlotTransferHistory { get; set; }
        public DbSet<PlotCancellation> PlotCancellation { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<PayType> PayType { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<PaymentPlan> PaymentPlan { get; set; }
        public DbSet<PaymentPlanDetail> PaymentPlanDetails { get; set; }
        public DbSet<PriceListNew> PriceListNew { get; set; }
        public DbSet<PriceListNewDetail> PriceListNewDetail { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenu { get; set; }
        public DbSet<ProductOpeningBalance> ProductOpeningBalance { get; set; }
        public DbSet<ApplicationReports> ApplicationReports { get; set; }
        public DbSet<RegisterationForms> RegisterationForms { get; set; }
        public DbSet<RegisterationFormDetail> RegisterationFormsDetail { get; set; }
    }
}
