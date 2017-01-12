using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCosmetics.Data.Models
{
    public class Gender
    {
        public Gender()
        {
            GenderCategories = new HashSet<GenderCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<GenderCategory> GenderCategories { get; set; }
    }
}
