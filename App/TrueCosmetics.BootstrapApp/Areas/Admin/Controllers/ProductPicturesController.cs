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

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class ProductPicturesController : AdminController<ProductPicture>
    {
        // GET: Admin/ProductPictures
        public async Task<ActionResult> Index()
        {
            return View(await Set.All().ToListAsync());
        }

        // GET: Admin/ProductPictures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPicture productPicture = await Set.FindAsync(id);
            if (productPicture == null)
            {
                return HttpNotFound();
            }
            return View(productPicture);
        }

        // GET: Admin/ProductPictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductPictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Path,IsPrimery")] ProductPicture productPicture)
        {
            if (ModelState.IsValid)
            {
                Set.Add(productPicture);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productPicture);
        }

        // GET: Admin/ProductPictures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPicture productPicture = await Set.FindAsync(id);
            if (productPicture == null)
            {
                return HttpNotFound();
            }
            return View(productPicture);
        }

        // POST: Admin/ProductPictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Path,IsPrimery")] ProductPicture productPicture)
        {
            if (ModelState.IsValid)
            {
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productPicture);
        }

        // GET: Admin/ProductPictures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPicture productPicture = await Set.FindAsync(id);
            if (productPicture == null)
            {
                return HttpNotFound();
            }
            return View(productPicture);
        }

        // POST: Admin/ProductPictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductPicture productPicture = await Set.FindAsync(id);
            Set.Delete(productPicture);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
