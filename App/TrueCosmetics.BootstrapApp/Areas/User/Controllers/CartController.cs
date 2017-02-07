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
using TrueCosmetics.BootstrapApp.Controllers;
using TrueCosmetics.BootstrapApp.Areas.User.Attributes;
using Microsoft.AspNet.Identity;
using TrueCosmetics.BootstrapApp.Areas.User.Models;

namespace TrueCosmetics.BootstrapApp.Areas.User.Controllers
{
    [Addressed]
    public class CartController : BaseController<Product, ApplicationUser, OrderStatus>
    {
        private static Dictionary<string, Order> CurrentOrders = new Dictionary<string, Order>();

        protected Order GetOrder()
        {
            var id = User.Identity.GetUserId();
            if (!CurrentOrders.Keys.Contains(id))
                CurrentOrders.Add(User.Identity.GetUserId(), new Order());

            return CurrentOrders[id];
        }

        public async Task<ActionResult> Add(int? productId)
        {
            if(!productId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var addressId = Session["AddressId"];
            var product = await Set.FindAsync(productId);
            var user = await SecondSet.All().FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            if (product == null || !user.Addresses.Any(x => x.Id.Equals(addressId)))
            {
                return HttpNotFound();
            }

            OrderDetailModel detail = new OrderDetailModel()
            {
                ProductName = product.Name,
                ProductId = product.Id,
                Quantity = 1
            };

            return View(detail);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "ProductId,Quantity")] OrderDetailModel order)
        {
            if (ModelState.IsValid)
            {
                var product = await Set.FindAsync(order.ProductId);
                if(product != null)
                {
                    var detail = GetOrder().OrderDetails.FirstOrDefault(x => x.ProductId == order.ProductId);
                    if(detail == null)
                    {
                        detail = new OrderDetail()
                        {
                            Order = GetOrder(),
                            Product = product,
                            ProductId = product.Id
                        };
                        GetOrder().OrderDetails.Add(detail);
                    }
                    detail.Quantity += order.Quantity;
                    return RedirectToAction("Cart");
                }
            }

            return View(order.ProductId);
        }
        
        public ActionResult Cart()
        {
            return View(GetOrder().OrderDetails.Select(OrderDetailModel.From).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckOut(IList<OrderDetailModel> details)
        {
            if(details.Count > 0)
            {
                Order order = GetOrder();
                order.OrderDetails.Clear();
                for (int i = 0; i < details.Count; i++)
                {
                    order.OrderDetails.Add(new OrderDetail()
                    {
                        Order = order,
                        Product = await Set.FindAsync(details[i].ProductId),
                        ProductId = details[i].ProductId,
                        Quantity = details[i].Quantity
                    });
                }
                order.OrderDate = DateTime.Now;
                order.OrderStatus = new OrderStatus()
                {
                    Status = Status.Pending,
                    DateChanged = DateTime.Now
                };
                var addrId = Session["AddressId"];
                var address = (await SecondSet.FindAsync(User.Identity.GetUserId()))
                    .Addresses.FirstOrDefault(x => x.Id.Equals(addrId));
                order.UserAddress = address;
                address.Orders.Add(order);
                await SaveChangesAsync();
                CurrentOrders.Remove(User.Identity.GetUserId());
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
