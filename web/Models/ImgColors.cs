using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ImgColors
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }  // Назва файлу зображення

        [Required]
        [ForeignKey("Animal")]
        public int ColorId { get; set; }
        public Animals Color { get; set; }
    }
}
