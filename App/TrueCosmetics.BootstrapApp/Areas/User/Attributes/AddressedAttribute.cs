using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TrueCosmetics.BootstrapApp.Areas.User.Attributes
{
    public class AddressedAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(base.AuthorizeCore(httpContext))
            {
                var addressId = httpContext.Session["AddressId"];
                var manager = httpContext.GetOwinContext().Get<ApplicationDbContext>().Users;
                var user = manager.Include(x => x.Addresses)
                    .FirstOrDefaultAsync(x => x.UserName == httpContext.User.Identity.Name).Result;
                if (!user.Addresses.Any(x => x.Id.Equals(addressId)))
                {
                    httpContext.Response.StatusCode = 415;
                    return false;
                }
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Response.StatusCode == 415)
            {
                filterContext.HttpContext.Session.Add("returnUrl", filterContext.HttpContext.Request.RawUrl);
                filterContext.Result = new RedirectResult("/User/Manage/Addresses");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}