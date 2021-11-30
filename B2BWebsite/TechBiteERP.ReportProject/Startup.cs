using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TechBiteERP.ReportProject.StartupOwin))]

namespace TechBiteERP.ReportProject
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
