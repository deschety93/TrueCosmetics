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
using Microsoft.AspNet.Identity.Owin;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.ApplicationUsers;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class ApplicationUsersController : UserAdminController<IdentityRole, ApplicationUserManager>
    {
        // GET: Admin/ApplicationUsers
        public async Task<ActionResult> Index()
        {
            var result = await UserManager.Users.Include(x => x.Roles).ToListAsync();
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();

            ViewBag.AllRoles = allRoles;
            return View(result.Select(x => ApplicationUserWithRolesModel.From(x, allRoles)));
        }
        // GET: Admin/ApplicationUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();
            ViewBag.AllRoles = allRoles;
            return View(ApplicationUserWithRolesModel.From(applicationUser, allRoles));
        }

        // GET: Admin/ApplicationUsers/Create
        public ActionResult Create()
        {
            ApplicationUser a = new ApplicationUser();
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();
            ViewBag.AllRoles = allRoles;
            return View(ApplicationUserWithRolesModel.From(a, allRoles));
        }

        // POST: Admin/ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,Password,PhoneNumber,RoleIds")] ApplicationUserWithRolesModel applicationUser)
        {
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();
            if (ModelState.IsValid)
            {
                ApplicationUser a = new ApplicationUser()
                {
                    Email = applicationUser.Email, PhoneNumber = applicationUser.PhoneNumber, UserName = applicationUser.Email
                };
                var result = await UserManager.CreateAsync(a, applicationUser.Password);
                if(result.Succeeded)
                {
                    await ApplicationUserWithRolesModel.To(applicationUser, a, UserManager, allRoles);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AllRoles = allRoles;
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();
            ViewBag.AllRoles = allRoles;
            return View(ApplicationUserWithRolesModel.From(applicationUser, allRoles));
        }

        // POST: Admin/ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,Password,PhoneNumber,RoleIds")] ApplicationUserWithRolesModel applicationUser)
        {
            var allRoles = Set.All().Select(ApplicationUserWithRolesModel.ConvertRoles).ToList();
            if (ModelState.IsValid)
            {
                ApplicationUser a = await UserManager.FindByIdAsync(applicationUser.Id);
                if(a.PasswordHash != applicationUser.Password)
                {
                    string token = await UserManager.GeneratePasswordResetTokenAsync(applicationUser.Id);
                    await UserManager.ResetPasswordAsync(applicationUser.Id, token, applicationUser.Password);
                }
                await ApplicationUserWithRolesModel.To(applicationUser, a, UserManager, allRoles);
                await UserManager.UpdateAsync(a);
                return RedirectToAction("Index");
            }
            ViewBag.AllRoles = allRoles;
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(applicationUser);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
