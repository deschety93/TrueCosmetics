using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.Orders;
using TrueCosmetics.Data;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class OrdersController : AdminController<Order, ApplicationUser, ApplicationUserAddress>
    {
        // GET: Admin/Orders
        public async Task<ActionResult> Index(Status? status, string userId, int? productId) 
        {
            var result = Set.All().Include(y => y.OrderStatus)
                .Include(x => x.UserAddress.User);
            if (status.HasValue)
                result = result.Where(x => x.OrderStatus.Status == status);
            if (!string.IsNullOrEmpty(userId))
                result = result.Where(x => x.UserAddress.User.Id == userId);
            if (productId.HasValue)
                result = result.Include(x => x.OrderDetails)
                               .Where(x => x.OrderDetails.Any(y => y.ProductId == productId));
            if (!string.IsNullOrEmpty(userId))
                return PartialView((await result.ToListAsync()).Select(OrderWithAddressModel.From));

            return View((await result.ToListAsync()).Select(OrderWithAddressModel.From));
        }

        // GET: Admin/Orders/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(OrderWithAddressModel.From(result));
        }

        // GET: Admin/Orders/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Users = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Email");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Comment,AddressId")] OrderWithAddressModel orderWithAddressModel)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order();
                if(await orderWithAddressModel.To(order, ThirdSet.All()))
                {
                    Set.Add(order);
                    await SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Users = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Email");
            return View(orderWithAddressModel);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await Set.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Email");
            return View(OrderWithAddressModel.From(order));
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,OrderDate,EstimatedDeliveryDate,Status,Comment,AddressId")] OrderWithAddressModel orderWithAddressModel)
        {
            if (ModelState.IsValid)
            {
                Order order = await Set.All().Include(x => x.OrderStatus).FirstOrDefaultAsync(x => x.Id == orderWithAddressModel.OrderId);
                if (order != null && await orderWithAddressModel.To(order, ThirdSet.All()))
                {
                    Set.Update(order);
                    await SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Users = new SelectList(await SecondSet.All().ToListAsync(), "Id", "Email");
            return View(orderWithAddressModel);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await Set.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(OrderWithAddressModel.From(order));
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await Set.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            Set.Delete(order);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
