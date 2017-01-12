using System.ComponentModel.DataAnnotations;

namespace TrueCosmetics.Data.Models
{
    public class ApplicationUserAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        public string Comment { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}