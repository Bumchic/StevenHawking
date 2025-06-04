using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSellApp.Models
{
    public class Order
    {
        [Key]
        public string Order_Id { get; set; }
        public DateTime DayBought { get; set; }

        [ForeignKey("User_Id")]
        public User? User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
    }
}
