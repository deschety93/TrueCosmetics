using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.Orders
{
    public class OrderWithProductModel
    {

        public static Func<OrderDetail, OrderWithProductModel> From = x =>
        {
            OrderWithProductModel result = new OrderWithProductModel()
            {
                OrderId = x.OrderId, ProductId = x.ProductId,
                ProductName = x.Product != null ? x.Product.Name : string.Empty,
                Quantity = x.Quantity
            };
            return result;
        };

        public async Task<bool> To(IQueryable<Product> products, IQueryable<Order> orders, OrderDetail detail)
        {
            Order o = await orders.Include(x => x.OrderStatus)
                .Include(x => x.UserAddress)
                .FirstOrDefaultAsync(x => x.Id == OrderId);
            Product p = await products.FirstOrDefaultAsync(x => x.Id == ProductId);
            if(o != null && p != null)
            {
                o.OrderStatus.DateChanged = DateTime.Now;
                if(o.OrderDetails.Count == 0)
                {
                    o.OrderDate = DateTime.Now;
                    o.OrderStatus.Status = Status.Pending;
                }
                o.OrderStatus.DeliveryDate = DateTime.Now.AddDays(3);
                detail.OrderId = o.Id;
                detail.ProductId = p.Id;
                detail.Product = p;
                detail.Order = o;
                detail.Quantity = Quantity;
                return true;
            }
            return false;
        }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}