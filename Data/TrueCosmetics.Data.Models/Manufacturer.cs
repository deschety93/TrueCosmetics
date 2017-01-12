using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrueCosmetics.Data.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}