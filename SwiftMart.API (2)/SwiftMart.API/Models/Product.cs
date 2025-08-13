using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftMart.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int Stock { get; set; } // ✅ Add this line

        public string? ImageUrl { get; set; }

        public string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public User Seller { get; set; }
    }
}
