using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Core;
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPage mainPage = new();
        private ProfilePage profilePage = new(); 
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(mainPage);
            Application.Current.MainWindow = this;
            if (UserContext.Current == null)
                profilePage.NicknameLbl.Content = "Guest";
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(mainPage);
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(profilePage);
        }
    }
}
