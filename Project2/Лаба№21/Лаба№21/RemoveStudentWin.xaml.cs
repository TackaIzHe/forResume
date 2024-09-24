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
    /// Логика взаимодействия для RemoveStudentWin.xaml
    /// </summary>
    public partial class RemoveStudentWin : Window
    {
        public int id = 0;

        public RemoveStudentWin()
        {
            InitializeComponent();
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            DbContext1 db = new DbContext1();
            if (id == 0)
            {
                db.Students.Remove(db.Students.Find(Convert.ToInt32(a1.Text)));
                db.SaveChanges();
            }

            if (id == 1)
            {
                db.Groups.Remove(db.Groups.Find(Convert.ToInt32(a1.Text)));
                db.SaveChanges();
            }

            if (id == 2)
            {
                db.Faculs.Remove(db.Faculs.Find(Convert.ToInt32(a1.Text)));
                db.SaveChanges();
            }
        }
    }
}
