using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrueCosmetics.Data.Models
{
    public class Category
    {
        public Category()
        {
            ChildCategories = new HashSet<Category>();
            GenderCategory = new HashSet<GenderCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<GenderCategory> GenderCategory { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }
        
        public virtual Category ParentCategory { get; set; }
    }
}