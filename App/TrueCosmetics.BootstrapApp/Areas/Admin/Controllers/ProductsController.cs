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
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.Products;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class ProductsController : AdminController<Product, Manufacturer, GenderCategory>
    {
        // GET: Admin/Products
        public async Task<ActionResult> Index()
        {
            var result = (await Set.All().Include(x => x.Manufacturer).Include(x => x.GenderCategories)
                .Include(x => x.GenderCategories.Select(y => y.Category))
                .Include(x => x.GenderCategories.Select(y => y.Gender))
                .ToListAsync()).Select(ProductWithPicModel.From);
            return View(result);
        }

        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Set.All().Include(x => x.Manufacturer).Include(x => x.GenderCategories).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(ProductWithPicModel.From(product));
        }

        // GET: Admin/Products/Create
        public async Task<ActionResult> Create()
        {
            var categories = new List<SelectListItem>();
            (await ThirdSet.All().Include(x => x.Category).Include(x => x.Gender)
                .ToListAsync()).ForEach(x => categories.Add(new SelectListItem()
                {
                    Value = x.CategoryId + " " + x.GenderId,
                    Text = x.Category.Name + " за " + x.Gender.Name
                }));
            ViewBag.Gc = categories;
            ViewBag.Manufactureres = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,Price,QuantityInStore,GenderCategoriesIds,ManufacturerId,PostedFiles")] ProductWithPicModel product)
        {
            if (ModelState.IsValid)
            {
                var pr = new Product();
                await product.To(pr, ThirdSet.All(), SecondSet.All(), Server);
                Set.Add(pr);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var categories = new List<SelectListItem>();
            (await ThirdSet.All().Include(x => x.Category).Include(x => x.Gender)
                .ToListAsync()).ForEach(x => categories.Add(new SelectListItem()
                {
                    Value = x.CategoryId + " " + x.GenderId,
                    Text = x.Category.Name + " за " + x.Gender.Name
                }));
            ViewBag.Gc = categories;
            ViewBag.Manufactureres = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Set.All().Include(x => x.Manufacturer).Include(x => x.GenderCategories).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var categories = new List<SelectListItem>();
            (await ThirdSet.All().Include(x => x.Category).Include(x => x.Gender)
                .ToListAsync()).ForEach(x => categories.Add(new SelectListItem()
                {
                    Value = x.CategoryId + " " + x.GenderId,
                    Text = x.Category.Name + " за " + x.Gender.Name
                }));
            ViewBag.Gc = categories;
            ViewBag.Manufactureres = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Name");
            return View(ProductWithPicModel.From(product));
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,QuantityInStore,GenderCategoriesIds,ManufacturerId,PostedFiles")] ProductWithPicModel product)
        {
            if (ModelState.IsValid)
            {
                Product pr = await Set.All().Include(x => x.Manufacturer).Include(x => x.GenderCategories)
                    .Include(x => x.GenderCategories.Select(y => y.Category))
                    .Include(x => x.GenderCategories.Select(y => y.Gender))
                    .Include(x => x.Pictures)
                    .Include(x => x.Pictures.Select(y => y.Product))
                    .FirstOrDefaultAsync(x => x.Id == product.Id);

                if(pr == null)
                {
                    return HttpNotFound();
                }
                if(product.GenderCategoriesIds != null)
                {
                    while (pr.GenderCategories.Count > 0)
                    {
                        pr.GenderCategories.Remove(pr.GenderCategories.Last());
                    }
                    await SaveChangesAsync();
                }
                if (product.PostedFiles != null && product.PostedFiles.First() != null)
                {
                    while (pr.Pictures.Count > 0)
                    {
                        Context.ProductPictures.Remove(pr.Pictures.First());
                    }
                    await SaveChangesAsync();
                }
                await product.To(pr, ThirdSet.All(), SecondSet.All(), Server);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var categories = new List<SelectListItem>();
            (await ThirdSet.All().Include(x => x.Category).Include(x => x.Gender)
                .ToListAsync()).ForEach(x => categories.Add(new SelectListItem()
                {
                    Value = x.CategoryId + " " + x.GenderId,
                    Text = x.Category.Name + " за " + x.Gender.Name
                }));
            ViewBag.Gc = categories;
            ViewBag.Manufactureres = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Set.All().Include(x => x.Manufacturer).Include(x => x.GenderCategories).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(ProductWithPicModel.From(product));
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await Set.FindAsync(id);
            Set.Delete(product);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Pictures(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = Set.All().Include(x => x.Pictures).FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return HttpNotFound();
            }

            return View(product.Pictures.Select(x => x.Path).ToList());
        }
    }
}
