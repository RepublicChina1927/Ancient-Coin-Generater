using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ancient_Coin_Generater.Models_for_DTO
{
    public class ImageRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(255)]
        public int UserId { get; set; }

        [Required]
        public byte[]? ImageData { get; set; } // Assuming you're storing the image as binary data

        [Required]
        public int? Id { get; set; } // Assuming you have user authentication

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow; // Automatically set to current time
    }
}
