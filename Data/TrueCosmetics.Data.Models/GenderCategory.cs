using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public class GenderCategory
    {
        public GenderCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key, Column(Order = 1), ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Key, Column(Order = 2), ForeignKey("Gender")]
        public int GenderId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual Gender Gender { get; set; }

        public virtual GenderCategoryPicture Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}