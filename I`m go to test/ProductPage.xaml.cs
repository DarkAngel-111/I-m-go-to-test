using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I_m_go_to_test
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private List<Product> _allproducts;
        public ProductPage()
        {
            InitializeComponent();
            var currentProduct = Багманов_Чудо_ОбувьEntities.GetContext().Product.ToList();
            _allproducts = currentProduct;
            UpdateProduct();
            if (Session.CurrentUser == null)
            {
                TBoxSearch.IsEnabled = false;
                ComboFilBox.IsEnabled = false;
                ComboSortBox.IsEnabled = false;
                ZakazBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateProduct();

        private void ComboFilBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProduct();

        private void ComboSortBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProduct();
        private void UpdateProduct()
        {
            if (_allproducts == null) return;

            var query = _allproducts.AsEnumerable();

            //1. Поиск по названию и описанию
            string search = TBoxSearch?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => (p.ProductName != null && 
                p.ProductName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                ||
                (p.Description != null &&
                p.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }

            //2. Фильтрация по категории
            ComboBoxItem selected = ComboFilBox.SelectedItem as ComboBoxItem;
            if (selected != null)
            {
                string category = selected.Content.ToString();

                if (category != "Все категории")
                {
                    query = query.Where(p => p.Category != null &&
                                             p.Category.Category_name == category);
                }
            }

            //3. Сортировка по цене
            ComboBoxItem sort = ComboSortBox.SelectedItem as ComboBoxItem;
            if (sort != null)
            {
                string sortStr = sort.Content.ToString();
                if (sortStr == "По возрастанию")
                {
                    query = query.OrderBy(p => p.Price);
                } 
                else if (sortStr == "По убыванию")
                {
                    query = query.OrderByDescending(p => p.Price);
                }
            }

            ProductView.ItemsSource = query.ToList();
        }

        private void ZakazBtn_Click(object sender, RoutedEventArgs e)
        {
            CartWindow window = new CartWindow();
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }

        private void MenuAddProduct_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;

            if (menu == null) return;

            ContextMenu contextMenu = menu.Parent as ContextMenu;

            if (contextMenu == null) return;

            Grid grid = contextMenu.PlacementTarget as Grid;

            if (grid == null) return;

            Product product = grid.DataContext as Product;

            if (product == null) return;

            AddCartWindow window = new AddCartWindow(product);
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }
    }
}
