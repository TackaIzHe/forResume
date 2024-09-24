using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Лаба_21.data;




namespace Лаба_21
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbContext1 db = new DbContext1();

        public MainWindow()
        {
            InitializeComponent();
            UpdateDb();
        }


        async public void UpdateDb()
        {
            while(true)
            {
                await Task.Delay(1000);
            Students.DataContext = db.Students.ToList();
            Groups.DataContext = db.Groups.ToList();
            Faculs.DataContext = db.Faculs.ToList();
            }

        }

        private void AddStudentAction(object sender, RoutedEventArgs e)
        {
            AddStudent AddStudentWin = new AddStudent();
            AddStudentWin.a.Text = "name";
            AddStudentWin.b.Text = "age";
            AddStudentWin.c.Text = "isGroup";
            AddStudentWin.Show();
        }

        private void AddGroupAction(object sender, RoutedEventArgs e)
        {
            AddStudent AddStudentWin = new AddStudent();
            AddStudentWin.id = 1;
            AddStudentWin.a.Text = "name";
            AddStudentWin.b.Text = "nameStarosta";
            AddStudentWin.c.Text = "idFacul";
            AddStudentWin.Show();
        }

        private void AddFaculAction(object sender, RoutedEventArgs e)
        {
            AddStudent AddStudentWin = new AddStudent();
            AddStudentWin.id = 2;
            AddStudentWin.a.Text = "name";
            AddStudentWin.b.Text = "curs";
            AddStudentWin.c.Text = "col";
            AddStudentWin.Show();
        }


        private void RemoveStudentAction(object sender, RoutedEventArgs e)
        {
            RemoveStudentWin win = new RemoveStudentWin();
            win.a.Text = "StudentId";
            win.Show();
        }

        private void RemoveGroupAction(object sender, RoutedEventArgs e)
        {
            RemoveStudentWin win = new RemoveStudentWin();
            win.a.Text = "GroupId";
            win.id= 1;
            win.Show();
        }

        private void RemoveFaculAction(object sender, RoutedEventArgs e)
        {
            RemoveStudentWin win = new RemoveStudentWin();
            win.a.Text = "FaculId";
            win.id= 2;
            win.Show();
        }

    }
}
