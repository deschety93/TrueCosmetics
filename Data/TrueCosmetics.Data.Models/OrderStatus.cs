using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public enum Status
    {
        Constructed, Pending, Accepted, Rejected, Shipped
    }

    public class OrderStatus
    {
        [Key, ForeignKey("Order")]
        public int OrderID { get; set; }

        public Status Status { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [Required]
        public virtual Order Order { get; set; }
    }
}