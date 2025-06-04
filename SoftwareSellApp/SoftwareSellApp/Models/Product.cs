using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareSellApp.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        [Required]
        [StringLength(200)]
        public string productName { get; set; }
        public string? images { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal price { get; set; }
        [StringLength(1000)]
        public string? introduce { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}