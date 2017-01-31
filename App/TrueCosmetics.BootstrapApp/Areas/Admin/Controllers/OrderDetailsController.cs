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
using TrueCosmetics.BootstrapApp.Areas.Admin.Models.Orders;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class OrderDetailsController : AdminController<OrderDetail, Product, Order>
    {
        // GET: Admin/OrderDetails
        public async Task<ActionResult> Index(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            var orderDetails = await Set.All()
                .Include(o => o.Product)
                .Where(o => o.OrderId == id).ToListAsync();

            return View( orderDetails.Select(OrderWithProductModel.From));
        }

        // GET: Admin/OrderDetails/Create
        public async Task<ActionResult> Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order o = await ThirdSet.FindAsync(id);
            if(o == null)
            {
                return HttpNotFound();
            }

            ViewBag.Products = new SelectList(SecondSet.All(), "Id", "Name");
            return View(new OrderWithProductModel() { OrderId = o.Id });
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,ProductId,Quantity")] OrderWithProductModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetail od = new OrderDetail();
                if(await orderDetail.To(SecondSet.All(), ThirdSet.All(), od))
                {
                    Set.Add(od);
                    await SaveChangesAsync();
                    return RedirectToAction("Index", new { id = od.OrderId });
                }
            }

            ViewBag.Id = orderDetail.OrderId;
            ViewBag.Products = new SelectList(SecondSet.All(), "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await Set.All().Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);

            if (orderDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.Products = new SelectList(SecondSet.All(), "Id", "Name", orderDetail.ProductId);
            return View(OrderWithProductModel.From(orderDetail));
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,ProductId,Quantity")] OrderWithProductModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetail od = await Set.All().Include(x => x.Order)
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync(x => x.OrderId == orderDetail.OrderId && x.ProductId == orderDetail.ProductId);
                if(od != null)
                {
                    od.Quantity = orderDetail.Quantity;
                    Set.Update(od);
                    await SaveChangesAsync();
                    return RedirectToAction("Index", new { id = od.OrderId });
                }
            }

            ViewBag.Products = new SelectList(SecondSet.All(), "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await Set.All().Include(x => x.Product)
                .Include(x => x.Order).FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(OrderWithProductModel.From(orderDetail));
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? orderId, int? productId)
        {
            if(orderId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await Set.FindAsync(orderId, productId);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            Set.Delete(orderDetail);
            await SaveChangesAsync();
            return RedirectToAction("Index", new { id = orderId });
        }
    }
}
