using System;
using System.Linq;
using System.Windows;

namespace I_m_go_to_test
{
    public partial class AddCartWindow : Window
    {
        private Product _product;

        public AddCartWindow(Product product)
        {
            InitializeComponent();

            _product = product;

            ProductNameText.Text = product.ProductName;

            SizeBox.ItemsSource = product.ProductItems
                .Where(x => x.QuantityStock > 0)
                .OrderBy(x => x.Size.Size1)
                .ToList();

            SizeBox.SelectedIndex = 0;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ProductItems selectedSize = SizeBox.SelectedItem as ProductItems;

            if (selectedSize == null)
            {
                MessageBox.Show("Нет доступных размеров!");
                return;
            }

            int quantity;

            if (!int.TryParse(QuantityBox.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите правильное количество!");
                return;
            }

            CartItem oldItem = Cart.Items.FirstOrDefault(x =>
                x.ProductItem.ID_ProductItem == selectedSize.ID_ProductItem);

            int alreadyInCart = oldItem == null ? 0 : oldItem.Quantity;

            if (quantity + alreadyInCart > selectedSize.QuantityStock)
            {
                MessageBox.Show("Недостаточно товара на складе!");
                return;
            }

            if (oldItem != null)
            {
                oldItem.Quantity += quantity;
            }
            else
            {
                Cart.Items.Add(new CartItem
                {
                    Product = _product,
                    ProductItem = selectedSize,
                    Quantity = quantity
                });
            }

            MessageBox.Show("Товар добавлен в корзину!");

            Close();
        }
    }
}