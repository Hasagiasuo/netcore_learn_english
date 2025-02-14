using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Animals
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string English { get; set; }

        [Required]
        public string Ukrainian { get; set; }
    }
}
