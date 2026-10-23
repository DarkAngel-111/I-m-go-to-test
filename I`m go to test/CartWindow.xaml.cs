using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace I_m_go_to_test
{
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            UpdateCart();
        }

        private void UpdateCart()
        {
            CartGrid.ItemsSource = null;
            CartGrid.ItemsSource = Cart.Items.ToList();

            TotalText.Text = "Итого: " +
                Cart.Items.Sum(x => x.Sum).ToString("F2") + " руб.";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CartItem item = button.DataContext as CartItem;

            if (item != null)
            {
                Cart.Items.Remove(item);
                UpdateCart();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.Items.Count == 0)
            {
                MessageBox.Show("Корзина пустая!");
                return;
            }

            MessageBox.Show("Переходим к оформлению заказа!");
        }
    }
}