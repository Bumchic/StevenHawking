namespace SoftwareSellApp.Models
{
    public class Cart
    {
        public List<CartItem> listOfCartItem { get; set; }
        public Cart()
        {
            listOfCartItem = new List<CartItem>();
        }
    }
}
