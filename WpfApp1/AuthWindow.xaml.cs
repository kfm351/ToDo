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
using System.Windows.Shapes;
using WpfApp1.Core;
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var acc = DataBaseManager.GetUserByNick(NicknameTb.Text);
            if (acc == null)
            {
                MessageBox.Show("Такого аккаунта не существует");
                return;
            }

            if (acc.Password != PasswordPb.Password)
            {
                MessageBox.Show("Неверный пароль!");
                return;
            }

            UserContext.Current = acc;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataBaseManager.GetUserByNick(NicknameTb.Text) == null)
            {
                var acc = new User()
                { ID = Guid.NewGuid().ToString(), Nickname = NicknameTb.Text, Password = PasswordPb.Password };
                DataBaseManager.AddUser(acc);
                UserContext.Current = acc;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
                return;
            }

            MessageBox.Show("Такой аккаунт существует!");
        }

        private void GuestBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
