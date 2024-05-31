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
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ChangingNameWindow.xaml
    /// </summary>
    public partial class ChangingNameWindow : Window
    {
        public ChangingNameWindow()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataBaseManager.GetUserByNick(NicknameTb.Text) != null)
            {
                MessageBox.Show("Ник занят");
                return;
            }

            if (NicknameTb.Text == "")
            {
                MessageBox.Show("Заполните поле!");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
