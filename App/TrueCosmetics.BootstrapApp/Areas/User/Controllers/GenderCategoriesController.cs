using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.Data;
using TrueCosmetics.Data.Models;
using TrueCosmetics.BootstrapApp.Controllers;

namespace TrueCosmetics.BootstrapApp.Areas.User.Controllers
{
    public class GenderCategoriesController : BaseController<GenderCategory, Gender, Category>
    {
        // GET: User/GenderCategories
        public async Task<ActionResult> Navigation()
        {
            var genderCategories = (await ThirdSet.All()
                .Include(g => g.GenderCategory)
                .Include(g => g.GenderCategory.Select(x => x.Gender))
                .Include(g => g.ChildCategories)
                .Where(x => x.ParentCategory == null)
                .ToListAsync())
                .Select(x => new Category()
                {
                    Id = x.Id, Name = x.Name, GenderCategory = x.GenderCategory.Concat(x.ChildCategories.SelectMany(y => y.GenderCategory)).ToList()
                });
            return View(genderCategories);
        }

        // GET: User/GenderCategories
        public async Task<ActionResult> Index()
        {
            var genderCategories = Set.All().Include(g => g.Category).Include(g => g.Gender).Include(g => g.Picture);
            return View(await genderCategories.ToListAsync());
        }
    }
}
