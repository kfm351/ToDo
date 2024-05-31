using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.Data
{
    internal static class DataBaseManager
    {
        private static TasksEntities _dataBase = new TasksEntities();

        public static void AddUser(User user)
        {
            _dataBase.User.Add(user);
            SaveChanges();
        }

        public static void UpdateTask(MyTask task)
        {
            _dataBase.MyTask.AddOrUpdate(task);
            SaveChanges();
        }
        public static void DeleteTask(MyTask task)
        {
            _dataBase.MyTask.Remove(task);
            SaveChanges();
        }
        public static List<MyTask> GetTasks()
        {
            return _dataBase.MyTask.ToList().Where(x => x.User == UserContext.Current).ToList();
        }

        public static User GetUserByNick(string nickname)
        {
            return _dataBase.User.ToList().FirstOrDefault(x => x.Nickname == nickname);
        }

        public static void UpdateUser(User user)
        {
            _dataBase.User.AddOrUpdate(user);
            SaveChanges();
        }

        public static void AddTask(MyTask task)
        {
            _dataBase.MyTask.Add(task);
            SaveChanges();
        }

        public static void SaveChanges()
        {
            try
            {
                _dataBase.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var f in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} fail validation\n", f.Entry.Entity.GetType());
                    foreach (var err in f.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", err.PropertyName, err.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Fail: " + sb.ToString(), ex);
            }
        }
    }
}
