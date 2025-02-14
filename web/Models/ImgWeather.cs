using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ImgWeather
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }  // Назва файлу зображення

        [Required]
        [ForeignKey("Animal")]
        public int WeatherId { get; set; }
        public Animals Weather { get; set; }
    }
}
