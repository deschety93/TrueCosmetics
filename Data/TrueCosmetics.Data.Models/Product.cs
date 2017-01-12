using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrueCosmetics.Data.Models
{
    public class Product
    {
        public Product()
        {
            GenderCategories = new HashSet<GenderCategory>();
            Pictures = new HashSet<ProductPicture>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required, DefaultValue(0)]
        public decimal Price { get; set; }

        public virtual ICollection<GenderCategory> GenderCategories { get; set; }

        public virtual ICollection<ProductPicture> Pictures { get; set; }

        [Required]
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public int QuanityInStore { get; set; }
    }
}