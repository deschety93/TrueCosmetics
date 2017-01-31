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
    public class OrderStatusController : AdminController<OrderStatus, Order>
    {
        // GET: Admin/OrderStatus
        public async Task<ActionResult> Index()
        {
            var orderStatuses = Set.All().Include(o => o.Order);
            return View(await orderStatuses.ToListAsync());
        }

        // GET: Admin/OrderStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = await Set.FindAsync(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // GET: Admin/OrderStatus/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(SecondSet.All(), "Id", "Comment");
            return View();
        }

        // POST: Admin/OrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,Status,DateChanged,DeliveryDate")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                Set.Add(orderStatus);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(SecondSet.All(), "Id", "Comment", orderStatus.OrderID);
            return View(orderStatus);
        }

        // GET: Admin/OrderStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = await Set.FindAsync(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(SecondSet.All(), "Id", "Comment", orderStatus.OrderID);
            return View(orderStatus);
        }

        // POST: Admin/OrderStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,Status,DateChanged,DeliveryDate")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(SecondSet.All(), "Id", "Comment", orderStatus.OrderID);
            return View(orderStatus);
        }

        // GET: Admin/OrderStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = await Set.FindAsync(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // POST: Admin/OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderStatus orderStatus = await Set.FindAsync(id);
            Set.Delete(orderStatus);
            await SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
