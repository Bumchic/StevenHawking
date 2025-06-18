namespace SoftwareSellApp.Models
{
    public class Cart
    {
        public List<Product> listOfProduct { get; set; }
        public Cart()
        {
            listOfProduct = new List<Product>();
        }
        public Cart(List<Product> listOfProduct)
        {
            this.listOfProduct = listOfProduct;
        }
    }
}
