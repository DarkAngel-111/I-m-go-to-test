using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace I_m_go_to_test
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginEnter.Text.Trim();

            if (login == "")
            {
                MessageBox.Show("Введите логин!");
                return;
            }

            var db = Багманов_Чудо_ОбувьEntities.GetContext();

            User user = db.User.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                Session.CurrentUser = user;

                MessageBox.Show("Добро пожаловать, " + user.Username + "!");

                NavigationService.Navigate(new ProductPage());
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Session.CurrentUser = null;

            NavigationService.Navigate(new ProductPage());
        }
    }
}