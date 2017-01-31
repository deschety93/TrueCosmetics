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
    public class ApplicationUserAddressesController : AdminController<ApplicationUserAddress, ApplicationUser>
    {
        // GET: Admin/ApplicationUserAddresses
        public ActionResult Index(string userId, string selectedId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.userId = userId;
            ViewBag.returnUrl = Request.UrlReferrer;
            ViewBag.selectedId = selectedId;
            return View(Set.All().Where(x => x.ApplicationUserId == userId).ToList());
        }

        // GET: Admin/ApplicationUserAddresses/Details/5
        public async Task<ActionResult> Details(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserAddress applicationUserAddress = await Set.FindAsync(id);
            if (applicationUserAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = returnUrl;
            return View(applicationUserAddress);
        }

        // GET: Admin/ApplicationUserAddresses/Create
        public async Task<ActionResult> Create(string userId, string returnUrl)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var o = await SecondSet.FindAsync(userId);
            if (o == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = userId;
            ViewBag.returnUrl = returnUrl;
            return View(new ApplicationUserAddress() { ApplicationUserId = userId });
        }

        // POST: Admin/ApplicationUserAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ApplicationUserId,Country,City,Address,Comment")] ApplicationUserAddress applicationUserAddress, string returnUrl)
        {
            if (ModelState.IsValid || ModelState["User"].Errors.Count > 0)
            {
                var u = await SecondSet.FindAsync(applicationUserAddress.ApplicationUserId);
                if(u != null)
                {
                    applicationUserAddress.User = u;
                    Set.Add(applicationUserAddress);
                    await SaveChangesAsync();
                    return Redirect(returnUrl);
                }
            }
            ViewBag.userId = applicationUserAddress.ApplicationUserId;
            ViewBag.returnUrl = returnUrl;
            return View(applicationUserAddress);
        }

        // GET: Admin/ApplicationUserAddresses/Edit/5
        public async Task<ActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserAddress applicationUserAddress = await Set.FindAsync(id);
            if (applicationUserAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = returnUrl;
            return View(applicationUserAddress);
        }

        // POST: Admin/ApplicationUserAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ApplicationUserId,Id,Country,City,Address,Comment")] ApplicationUserAddress applicationUserAddress, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Set.Update(applicationUserAddress);
                await SaveChangesAsync();
                return RedirectToAction("Index", new { userId = applicationUserAddress .ApplicationUserId });
            }
            ViewBag.returnUrl = returnUrl;
            return View(applicationUserAddress);
        }

        // GET: Admin/ApplicationUserAddresses/Delete/5
        public async Task<ActionResult> Delete(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserAddress applicationUserAddress = await Set.FindAsync(id);
            if (applicationUserAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = returnUrl;
            return View(applicationUserAddress);
        }

        // POST: Admin/ApplicationUserAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            ApplicationUserAddress applicationUserAddress = await Set.FindAsync(id);
            Set.Delete(applicationUserAddress);
            await SaveChangesAsync();
            return Redirect(returnUrl);
        }
    }
}
