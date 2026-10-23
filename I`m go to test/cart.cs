using System.Collections.Generic;
using System.Linq;

namespace I_m_go_to_test
{
    public class CartItem
    {
        public Product Product { get; set; }
        public ProductItems ProductItem { get; set; }
        public int Quantity { get; set; }

        public string Name
        {
            get { return Product.ProductName; }
        }
        public double Size
        {
            get { return ProductItem.Size.Size1; }
        }
        public decimal Price
        {
            get { return Product.Price; }
        }
        public decimal Sum
        {
            get { return Price * Quantity; }
        }
    }

    public static class Cart
    {
        public static List<CartItem> Items = new List<CartItem>();
    }
}