using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCosmetics.Data.Models
{
    public class ProductPicture
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}
