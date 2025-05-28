namespace SoftwareSellApp.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime DayBought { get; set; }
        public User? from { get; set; }
    }
}
