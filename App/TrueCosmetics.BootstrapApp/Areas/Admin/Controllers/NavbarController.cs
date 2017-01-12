using TrueCosmetics.BootstrapApp.Areas.Admin.Domain;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class NavbarController : AdminController
    {
        // GET: Navbar
        public ActionResult Index()
        {
            var data = new AdminNavData();
            return PartialView("_Navbar", data.navbarItems().ToList());
        }
    }
}