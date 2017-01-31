using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.Orders
{
    public class OrderWithAddressModel
    {
        public static Func<Order, OrderWithAddressModel> From = x =>
        {
            OrderWithAddressModel result = new OrderWithAddressModel()
            {
                OrderId = x.Id,
                OrderDate = x.OrderDate,
                Comment = x.Comment
            };
            if(x.OrderStatus != null)
            {
                result.EstimatedDeliveryDate = x.OrderStatus.DeliveryDate;
                result.DateChanged = x.OrderStatus.DateChanged;
                result.Status = x.OrderStatus.Status;
            }
            if (x.UserAddress != null)
            {
                result.AddressId = x.UserAddress.Id;
                result.Country = x.UserAddress.Country;
                result.City = x.UserAddress.City;
                result.Address = x.UserAddress.Address;
                if(x.UserAddress.User != null)
                {
                    result.UserId = x.UserAddress.User.Id;
                    result.UserEmail = x.UserAddress.User.Email;
                }
            }
            return result;
        };

        public async Task<bool> To(Order order, IQueryable<ApplicationUserAddress> addresses)
        {
            var address = await addresses.FirstOrDefaultAsync(x => x.Id == AddressId);
            if(address != null)
            {
                order.UserAddressId = address.Id;
                order.UserAddress = address;
                if (order.OrderStatus == null)
                {
                    order.OrderStatus = new OrderStatus()
                    {
                        Order = order,  OrderID = order.Id,
                    };
                }
                order.OrderStatus.DateChanged = DateTime.Now;
                order.OrderStatus.Status = Status;
                order.Comment = Comment;
                return true;
            }
            return false;
        }

        [Key]
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? DateChanged { get; set; }
        
        public Status Status { get; set; }

        public string Comment { get; set; }
        
        public int AddressId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }
    }
}