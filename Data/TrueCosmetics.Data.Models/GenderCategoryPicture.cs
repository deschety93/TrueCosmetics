using System.ComponentModel.DataAnnotations;

namespace TrueCosmetics.Data.Models
{
    public class GenderCategoryPicture
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public virtual GenderCategory GenderCategory { get; set; }
    }
}