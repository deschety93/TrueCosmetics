using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TrueCosmetics.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using TrueCosmetics.Data.Common.Repositories;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models;
using System.Data.Entity.Core.Objects;
using System.Dynamic;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
    }

    public class AdminController<T> : AdminController where T : class
    {
        protected ApplicationDbContext Context;

        protected IRepository<T> Set { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            Context = requestContext.HttpContext.GetOwinContext().GetUserManager<ApplicationDbContext>();
            Set = new EfGenericRepository<T>(Context);
            base.Initialize(requestContext);
        }

        protected virtual async Task SaveChangesAsync()
        {
            try
            {
                foreach (var entry in Context.ChangeTracker.Entries()
                    .Where(x => x.State != EntityState.Detached && x.State != EntityState.Unchanged).Distinct())
                {
                    ChangeNotification.AllNotifications.AddFirst(new ChangeNotification()
                    {
                        Name = ObjectContext.GetObjectType(entry.Entity.GetType()).Name,
                        State = entry.State,
                        ChangeDate = DateTime.Now
                    });

                    if(ChangeNotification.AllNotifications.Count > 10)
                    {
                        ChangeNotification.AllNotifications.RemoveLast();
                    }
                }
                await Set.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Set.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class UserAdminController<F, S> : AdminController<F>  where F : class where S : class
    {
        protected S UserManager { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            UserManager = requestContext.HttpContext.GetOwinContext().GetUserManager<S>();
        }
    }

    public class AdminController<F, S> : AdminController<F> where F : class where S : class
    {
        protected IRepository<S> SecondSet { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SecondSet = new EfGenericRepository<S>(Context);
        }
    }

    public class AdminController<F, S, T> : AdminController<F, S> where F : class where S : class where T : class
    {
        protected IRepository<T> ThirdSet { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ThirdSet = new EfGenericRepository<T>(Context);
        }
    }
}