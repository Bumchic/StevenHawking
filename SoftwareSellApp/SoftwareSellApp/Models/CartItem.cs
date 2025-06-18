using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareSellApp.Models
{
    public class CartItem : Product
    {
        public int Quantity { get; set; }
        public decimal Total => price * Quantity;
    }
}