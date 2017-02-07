using System.Web.Mvc;
using TrueCosmetics.BootstrapApp.Controllers;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
    }
    [Authorize(Roles = "Admin")]
    public class AdminController<T> : BaseController<T> where T : class
    {
        
    }

    [Authorize(Roles = "Admin")]
    public class UserAdminController<F, S> : UserBaseController<F, S>  where F : class where S : class
    {
    }
    [Authorize(Roles = "Admin")]
    public class AdminController<F, S> : BaseController<F, S> where F : class where S : class
    {
    }
    [Authorize(Roles = "Admin")]
    public class AdminController<F, S, T> : BaseController<F, S, T> where F : class where S : class where T : class
    {
    }
}