using System.Web.Mvc;

namespace TrueCosmetics.BootstrapApp.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TrueCosmetics.BootstrapApp.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";
        }
    }
}