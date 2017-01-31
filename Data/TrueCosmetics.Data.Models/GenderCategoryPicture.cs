using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueCosmetics.Data.Models
{
    public class GenderCategoryPicture
    {
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }

        [Key, Column(Order = 2)]
        public int GenderId { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public virtual GenderCategory GenderCategory { get; set; }
    }
}