using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.User.Models
{
    public class OrderDetailModel
    {
        public static Func<OrderDetail, OrderDetailModel> From = x =>
        {
            OrderDetailModel result = new OrderDetailModel()
            {
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductPrice = x.Product.Price,
                Quantity = x.Quantity
            };
            return result;
        };

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
    }
}