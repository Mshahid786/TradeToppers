using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TechBiteERP
{
    public class Startup
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        [Obsolete]
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            ConfigurationRoot = new ConfigurationBuilder()
                           .SetBasePath(hostingEnvironment.ContentRootPath)
                           .AddJsonFile("appsettings.json")
                           .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-NZ");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("en-NZ") };
            });
             
            services.AddDbContext<ApplicationDbContext>(options =>
                              options.UseSqlServer(ConfigurationRoot.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
           //  services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            //services.AddRazorPages();
            services.AddRazorPages()
                    .AddMvcOptions(options =>
                                   {
                                       options.MaxModelValidationErrors = 50;
                                       options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                                           _ => "The field is required.");
                                   });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true; // here 1
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);// here 2
            });
            //services.ConfigureApplicationCookie(o =>
            //{
            //    o.ExpireTimeSpan = TimeSpan.FromSeconds(10);
            //    o.SlidingExpiration = true;
            //});
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           // app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapRazorPages();

               
                endpoints.MapAreaControllerRoute(
                   name: "areasGeneralLedger",
                   areaName: "GeneralLedger",
                   pattern: "{GeneralLedger}/{controller=ChartofAccount}/{action=Index}/{id?}"
                   );
                endpoints.MapAreaControllerRoute(
                   name: "MyAreaAccountReceivable",
                   areaName: "AccountReceivable",
                   pattern: "{AccountReceivable}/{controller=SalesMan}/{action=Index}/{id?}"
                   );
                endpoints.MapAreaControllerRoute(
                  name: "MyAreaAccountPayable",
                  areaName: "AccountPayable",
                  pattern: "{AccountPayable}/{controller=Supplier}/{action=Index}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                  name: "MyAreaHumanResource",
                  areaName: "HumanResource",
                  pattern: "{HumanResource}/{controller=Area}/{action=Index}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                  name: "MyAreaInventory",
                  areaName: "Inventory",
                  pattern: "{Inventory}/{controller=Item}/{action=Index}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                  name: "MyAreaTradeTopper",
                  areaName: "TradeTopper",
                  pattern: "{TradeTopper}/{controller=Area}/{action=Counseling}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                  name: "MyAreaRealEstate",
                  areaName: "RealEstate",
                  pattern: "{RealEstate}/{controller=Sale}/{action=Index}/{id?}"
                  );
                //endpoints.MapAreaControllerRoute(
                //    name: "areas",
                //    areaName: "areas",
                //    pattern: "{area}/{controller=Account}/{action=Login}/{id?}"
                //    );
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "/{controller=Dashboard}/{action=Index}/{id?}"
                   );

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "/{controller=Report}/{action=Print}/{id?}"
                  );
            });
        }
    }
}
