using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ImgAnimals
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }  // Назва файлу зображення

        [Required]
        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public Animals Animal { get; set; }
    }
}
