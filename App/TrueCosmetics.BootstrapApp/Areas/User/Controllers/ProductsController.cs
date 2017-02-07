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
using TrueCosmetics.BootstrapApp.Areas.User.Models;

namespace TrueCosmetics.BootstrapApp.Areas.User.Controllers
{
    public class ProductsController : BaseController<Product, Manufacturer, GenderCategory>
    {
        // GET: User/Products
        public async Task<ActionResult> Index(int? genderId, int? categoryId, int? manufacturerId, string gender)
        {
            var products = Set.All()
                .Include(p => p.Manufacturer)
                .Include(p => p.GenderCategories);
            if (genderId.HasValue)
                products = products.Where(x => x.GenderCategories.Any(y => y.GenderId == genderId));

            if (categoryId.HasValue)
                products = products.Where(x => x.GenderCategories.Any(y => y.CategoryId == categoryId));

            if (manufacturerId.HasValue)
                products = products.Where(x => x.ManufacturerId == manufacturerId);
            if (!string.IsNullOrEmpty(gender))
                products = products.Where(x => x.GenderCategories.Any(y => y.Gender.Name == gender));

            if (Request.IsAjaxRequest())
                return PartialView(await products.ToListAsync());
            await GatherFilters(products);
            return View(await products.ToListAsync());
        }

        public async Task<ActionResult> ProductFilters()
        {
            await GatherFilters(Set.All());
            return PartialView("_ProductFilters", new ProductFilterModel()
            {
                PriceFrom = (decimal)ViewData["minPrice"],
                PriceTo = (decimal)ViewData["maxPrice"],
            });
        }

        private async Task GatherFilters(IQueryable<Product> products)
        {
            ViewBag.Manufacturers = await SecondSet.All().ToListAsync();
            List<SelectListItem> catFilter = new List<SelectListItem>();
            List<SelectListItem> manFilter = new List<SelectListItem>();
            products.SelectMany(x => x.GenderCategories).Distinct().ToList().ForEach(x =>
            {
                catFilter.Add(new SelectListItem()
                {
                    Value = x.CategoryId + " " + x.GenderId,
                    Text = x.Category.Name + " за " + x.Gender.Name
                });
            });
            products.Select(x => x.Manufacturer).Distinct().ToList().ForEach(x =>
            {
                manFilter.Add(new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            });
            ViewData.Add("catFilter", catFilter);
            ViewData.Add("manFilter", manFilter);
            ViewData.Add("minPrice", products.Min(x => x.Price));
            ViewData.Add("maxPrice", products.Max(x => x.Price));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ProductFilterModel filter)
        {
            var products = Set.All()
                .Include(p => p.Manufacturer)
                .Include(p => p.GenderCategories);
            if(filter.CategoryIds != null)
            {
                var category = filter.CategoryIds.Select(x =>
                {
                    var keys = x.Split().Select(y => int.Parse(y)).ToArray();
                    return new GenderCategory()
                    {
                        CategoryId = keys[0],
                        GenderId = keys[1]
                    };
                });
                products = category
                    .Join(products.SelectMany(x => x.GenderCategories),
                        x => new { x.CategoryId, x.GenderId },
                        y => new { y.CategoryId, y.GenderId },
                        (x, y) => y.Products).SelectMany(x => x)
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .AsQueryable();
            }
            if (filter.ManufacturerIds != null)
            {
                products = products.Where(x => filter.ManufacturerIds.Contains(x.Id));
            }
            if (filter.PriceFrom < filter.PriceTo)
            {
                products = products.Where(x => x.Price >= filter.PriceFrom && x.Price <= filter.PriceTo );
            }
            await GatherFilters(products);
            return View(products.ToList());
        }

        // GET: User/Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Set.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
