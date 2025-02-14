using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ImgFruits
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }  // Назва файлу зображення

        [Required]
        [ForeignKey("Animal")]
        public int FruitId { get; set; }
        public Animals Fruit { get; set; }
    }
}
