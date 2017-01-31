using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public class ApplicationUserAddress
    {
        public ApplicationUserAddress()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        public string Comment { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}