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
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.Manufacturers;
using System.IO;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class ManufacturersController : AdminController<Manufacturer>
    {
        // GET: Admin/Manufacturers
        public async Task<ActionResult> Index()
        {
            return View((await Set.All().ToListAsync()).Select(ManufacturerWithPicModel.From));
        }

        // GET: Admin/Manufacturers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await Set.FindAsync(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Admin/Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,PostedFile")] ManufacturerWithPicModel manufacturer)
        {
            if (ModelState.IsValid)
            {
                Manufacturer g = await Set.FindAsync(manufacturer.Id);
                if (g == null)
                {
                    Set.Add(new Manufacturer()
                    {
                        Name = manufacturer.Name,
                        Description = manufacturer.Description,
                        LogoPicPath = SetPicture(manufacturer.PostedFile)
                    });
                    await SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        // GET: Admin/Manufacturers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result  = await Set.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(ManufacturerWithPicModel.From(result));
        }

        // POST: Admin/Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,PostedFile")] ManufacturerWithPicModel manufacturer)
        {
            if (ModelState.IsValid)
            {
                var result = await Set.FindAsync(manufacturer.Id);
                if(result == null)
                    return HttpNotFound();
                result.Name = manufacturer.Name;
                result.Description = manufacturer.Description;
                if (manufacturer.PostedFile != null)
                    result.LogoPicPath = SetPicture(manufacturer.PostedFile);
                Set.Update(result);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = manufacturer.Id });
        }

        // GET: Admin/Manufacturers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await Set.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(ManufacturerWithPicModel.From(result));
        }

        // POST: Admin/Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = await Set.FindAsync(id);
            Set.Delete(manufacturer);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private string SetPicture(HttpPostedFileBase photo)
        {
            if (photo == null)
                return "";

            string path = "~/Content/images/Manufacturers/" + Path.GetFileName(photo.FileName);
            photo.SaveAs(Server.MapPath(path));
            return path;
        }
    }
}
