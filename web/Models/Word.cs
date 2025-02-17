using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string English { get; set; }

        [Required, MaxLength(50)]
        public string Ukrainian { get; set; }

        public byte[]? Image { get; set; }  

        [Required, MaxLength(50)]
        public string Category { get; set; } 
    }
}
