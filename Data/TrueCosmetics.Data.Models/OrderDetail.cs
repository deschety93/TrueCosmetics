using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 1), ForeignKey("Order")]
        public int OrderId { get; set; }

        [Key, Column(Order = 2), ForeignKey("Product")]
        public int ProductId { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}