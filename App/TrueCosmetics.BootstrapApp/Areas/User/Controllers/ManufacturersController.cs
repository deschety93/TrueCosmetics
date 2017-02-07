using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.BootstrapApp.Controllers;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.User.Controllers
{
    public class ManufacturersController : BaseController<Manufacturer>
    {
        // GET: User/Manufacturers
        public ActionResult Index()
        {
            return View();
        }
        // GET: User/GenderCategories
        public async Task<ActionResult> Navigation()
        {
            var result = await Set.All().ToListAsync();
            return View(result);
        }
    }
}