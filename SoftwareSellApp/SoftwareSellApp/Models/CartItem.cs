using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareSellApp.Models
{
    public class CartItem
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public string? images { get; set; }
        public decimal Total => price * Quantity;
    }
}