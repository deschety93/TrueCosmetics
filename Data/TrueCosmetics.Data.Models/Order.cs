using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAddress")]
        public int UserAddressId { get; set; }

        [Required]
        public virtual ApplicationUserAddress UserAddress { get; set; }

        public DateTime? OrderDate { get; set; }

        [Required]
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public string Comment { get; set; }
    }
}