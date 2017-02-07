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
using System.IO;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.GenderCategories;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class GenderCategoriesController : AdminController<GenderCategory, Gender, Category>
    {
        // GET: Admin/GenderCategories
        public async Task<ActionResult> Index(int? categoryId)
        {
            var genderCategories = await Set.All()
                .Include(g => g.Category)
                .Include(g => g.Gender)
                .Include(g => g.Picture)
                .Where(g => categoryId != null ? g.CategoryId == categoryId : true )
                .ToListAsync();
            ViewBag.categoryId = categoryId;
            return View(genderCategories.Select(GenderCategoryWithPicModel.From));
        }

        // GET: Admin/GenderCategories/Details/5
        public async Task<ActionResult> Details(int? CategoryId, int? GenderId)
        {
            if (CategoryId == null || GenderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenderCategory genderCategory = await Set.FindAsync(CategoryId, GenderId);
            if (genderCategory == null)
            {
                return HttpNotFound();
            }
            return View(GenderCategoryWithPicModel.From(genderCategory));
        }

        // GET: Admin/GenderCategories/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(ThirdSet.All(), "Id", "Name");
            ViewBag.Genders = new SelectList(SecondSet.All(), "Id", "Name");
            return View();
        }

        // POST: Admin/GenderCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,GenderId,PostedFile")] GenderCategoryWithPicModel genderCategoryModel)
        {
            if (ModelState.IsValid)
            {
                GenderCategory g = await Set.FindAsync(genderCategoryModel.CategoryId, genderCategoryModel.GenderId);
                if(g == null)
                {
                    var gender = await SecondSet.FindAsync(genderCategoryModel.GenderId);
                    var category = await ThirdSet.FindAsync(genderCategoryModel.CategoryId);
                    Set.Add(new GenderCategory()
                    {
                        CategoryId = genderCategoryModel.CategoryId,
                        GenderId = genderCategoryModel.GenderId,
                        Gender = gender,
                        Category = category,
                        Picture = SetPicture(null, genderCategoryModel.PostedFile)
                    });
                    await SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        // GET: Admin/GenderCategories/Edit/5
        public async Task<ActionResult> Edit(int? CategoryId, int? GenderId)
        {
            if (CategoryId == null || GenderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenderCategory genderCategory = await Set.All()
                .Include(x => x.Category).Include(x => x.Gender).Include(x => x.Picture).FirstOrDefaultAsync(
                    x => x.CategoryId == CategoryId && x.GenderId == GenderId 
                );
            if (genderCategory == null)
            {
                return HttpNotFound();
            }
            return View(GenderCategoryWithPicModel.From(genderCategory));
        }

        // POST: Admin/GenderCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,GenderId,PostedFile")] GenderCategoryWithPicModel genderCategoryModel)
        {
            if (ModelState.IsValid)
            {
                if(genderCategoryModel.PostedFile != null)
                {
                    GenderCategory g = await Set.All()
                        .Include(x => x.Category)
                        .Include(x => x.Gender)
                        .Include(x => x.Picture)
                        .FirstOrDefaultAsync(
                            x => x.CategoryId == genderCategoryModel.CategoryId && x.GenderId == genderCategoryModel.GenderId
                    );
                    g.Picture = SetPicture(g, genderCategoryModel.PostedFile);
                    Set.Update(g);
                    await SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { CategoryId = genderCategoryModel.CategoryId, GenderId = genderCategoryModel.GenderId });
        }

        // GET: Admin/GenderCategories/Delete/5
        public async Task<ActionResult> Delete(int? CategoryId, int? GenderId)
        {
            if (CategoryId == null || GenderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenderCategory genderCategory = await Set.All()
                .Include(x => x.Category).Include(x => x.Gender).Include(x => x.Picture).FirstOrDefaultAsync(
                    x => x.CategoryId == CategoryId && x.GenderId == GenderId
            );
            if (genderCategory == null)
            {
                return HttpNotFound();
            }
            return View(GenderCategoryWithPicModel.From(genderCategory));
        }

        // POST: Admin/GenderCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? CategoryId, int? GenderId)
        {
            GenderCategory genderCategory = await Set.FindAsync(CategoryId, GenderId);
            Set.Delete(genderCategory);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private GenderCategoryPicture SetPicture(GenderCategory gc, HttpPostedFileBase photo)
        {
            if (photo == null)
                return null;

            GenderCategoryPicture pic = new GenderCategoryPicture();
            if (gc != null && gc.Picture != null)
            {
                pic = gc.Picture;
            }
            pic.Name = Path.GetFileName(photo.FileName);
            pic.Path = "~/Content/images/GenderCategories/" + pic.Name;
            photo.SaveAs(Server.MapPath(pic.Path));
            return pic;
        }
    }
}
