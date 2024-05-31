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
using WpfApp1.Core;
using WpfApp1.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<MyTask> _myTasks = [];
        private List<MyTask> _filtered = [];
        public MainPage()
        {
            InitializeComponent();
            FilterCb.ItemsSource = new List<string>() { "", "Завершенные", "Незавершенные", "Неважные", "Обычный приоритет", "Средний приоритет", "Высокий приоритет", "Наивысший приоритет" };
            SortCb.ItemsSource = new List<string>() { "", "По важности", "По важности по убыванию", "По дате добавления", "По дате добавления по убыванию" };

            _myTasks = DataBaseManager.GetTasks();
            LstView.DataContext = this;
            LstView.ItemsSource = _myTasks;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var task = new MyTask()
            {
                ID = Guid.NewGuid().ToString(),
                Task = TaskContentTb.Text,
                Priority = 0,
                User_ID = UserContext.Current is null ? "" : UserContext.Current.ID,
                WasCreated = DateTime.Now
            };
            if (UserContext.Current != null)
                DataBaseManager.AddTask(task);
            TaskContentTb.Text = "";
            _myTasks.Add(task);
            Filter();
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            var task = LstView.SelectedItem as MyTask;
            if (task == null) return;
            _myTasks.Remove(task);
            if (UserContext.Current != null) DataBaseManager.DeleteTask(task);
            Filter();
        }

        private void IncreaseMI_OnClick(object sender, RoutedEventArgs e)
        {
            var task = LstView.SelectedItem as MyTask;
            if (task == null) return;
            if (task.Priority == 4) return;
            task.Priority++;
            if (UserContext.Current != null) DataBaseManager.UpdateTask(task);
            Filter();
        }

        private void DecreaseMI_OnClick(object sender, RoutedEventArgs e)
        {
            var task = LstView.SelectedItem as MyTask;
            if (task == null) return;
            if (task.Priority == 0) return;
            task.Priority--;
            if (UserContext.Current != null) DataBaseManager.UpdateTask(task);
            Filter();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var task = LstView.SelectedItem as MyTask;
            if (task == null) return;
            task.Done = true;
            if (UserContext.Current != null) DataBaseManager.UpdateTask(task);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var task = LstView.SelectedItem as MyTask;
            if (task == null) return;
            task.Done = false;
            if (UserContext.Current != null) DataBaseManager.UpdateTask(task);
        }

        private void FilterCb_DropDownClosed(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            _filtered = _myTasks;
            var priority = FilterCb.SelectedItem as string;
            if (priority != "")
            {
                switch (priority)
                {
                    case "Завершенные":
                        _filtered = _filtered.Where(x => x.Done).ToList();
                        break;
                    case "Незавершенные":
                        _filtered = _filtered.Where(x => x.Done == false).ToList();
                        break;
                    case "Неважные":
                        _filtered = _filtered.Where(x => x.Priority == 0 && !x.Done).ToList();
                        break;
                    case "Обычный приоритет":
                        _filtered = _filtered.Where(x => x.Priority == 1 && !x.Done).ToList();
                        break;
                    case "Средний приоритет":
                        _filtered = _filtered.Where(x => x.Priority == 2 && !x.Done).ToList();
                        break;
                    case "Высокий приоритет":
                        _filtered = _filtered.Where(x => x.Priority == 3 && !x.Done).ToList();
                        break;
                    case "Наивысший приоритет":
                        _filtered = _filtered.Where(x => x.Priority == 4 && !x.Done).ToList();
                        break;
                }
            }

            if (SortCb.SelectedItem is string sort)
            {
                switch (sort)
                {
                    case "По важности":
                        _filtered = _filtered.OrderBy(x => x.Priority).ToList();
                        break;
                    case "По важности по убыванию":
                        _filtered = _filtered.OrderByDescending(x => x.Priority).ToList();
                        break;
                    case "По дате добавления":
                        _filtered = _filtered.OrderBy(x => x.WasCreated).ToList();
                        break;
                    case "По дате добавления по убыванию":
                        _filtered = _filtered.OrderByDescending(x => x.WasCreated).ToList();
                        break;
                }
            }

            LstView.ItemsSource = _filtered.Where(x => x.Task.ToLower().Contains(SearchTb.Text));
            LstView.Items.Refresh();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void SortCb_DropDownClosed(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
