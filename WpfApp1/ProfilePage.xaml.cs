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
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            if (UserContext.Current != null) 
                NicknameLbl.Content = UserContext.Current.Nickname;
            if (UserContext.Current == null)
                ChangeNameBtn.IsEnabled = false;
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            UserContext.Current = null;
            var auth = new AuthWindow();
            auth.Show();
            Application.Current.MainWindow.Close();
            

        }

        private void ChangeNameBtn_Click(object sender, RoutedEventArgs e)
        {
            var changing = new ChangingNameWindow();
            changing.ShowDialog();
            if (changing.DialogResult == true)
            {
                UserContext.Current.Nickname = changing.NicknameTb.Text;
                DataBaseManager.UpdateUser(UserContext.Current);
            }

            NicknameLbl.Content = UserContext.Current.Nickname;
        }
    }
}
