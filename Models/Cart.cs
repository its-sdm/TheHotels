using System.ComponentModel.DataAnnotations;

namespace TheHotels.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdHotel { get; set; }
        [Required]
        public int IdRoom { get; set; }
        [Required]
        public int IdRoomDetails { get; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
