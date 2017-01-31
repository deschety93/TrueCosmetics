using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCosmetics.Data.Models
{
    public class ProductPicture
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [DefaultValue(false)]
        public bool IsPrimery { get; set; }
    }
}
