namespace SoftwareSellApp.Models
{
    public class Customer: UserAbstract
    {
        public List<Order> OrderHistory { get; set; }

    }
}
