using System.Web.Mvc;

namespace TrueCosmetics.BootstrapApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TrueCosmetics.BootstrapApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
        }
    }
}