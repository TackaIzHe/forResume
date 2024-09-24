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
using Лаба_21.data;

namespace Лаба_21
{
    /// <summary>
    /// Логика взаимодействия для AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public int id = 0;
        public AddStudent()
        {
            InitializeComponent();
            
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (id == 0)
            {
                DbContext1 db = new DbContext1();
                Student student = new Student { name = a1.Text.ToString(), age = Convert.ToInt32(b1.Text), idGroup = Convert.ToInt32(c1.Text) };
                db.Students.Add(student);
                db.SaveChanges();
            }
            if (id == 1)
            {
                DbContext1 db = new DbContext1();
                Group student = new Group { name = a1.Text.ToString(), nameStarosta = b1.Text, idFacul = Convert.ToInt32(c1.Text) };
                db.Groups.Add(student);
                db.SaveChanges();
            }
            if (id == 2)
            {
                DbContext1 db = new DbContext1();
                Facul student = new Facul { name = a1.Text.ToString(), curs = Convert.ToInt32(b1.Text), col = Convert.ToInt32(c1.Text) };
                db.Faculs.Add(student);
                db.SaveChanges();
            }
            
            
        }
    }
}
