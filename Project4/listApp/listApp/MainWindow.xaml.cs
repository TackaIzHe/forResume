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

namespace listApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        

        private void Stud_Click(object sender, RoutedEventArgs e)
        {
            
            Stud.Items.Add(new TreeViewItem() { Header = TextBoxx.Text });
            
        }

        private void Zach_Click(object sender, RoutedEventArgs e)
        {
            Zach.Items.Add(new TreeViewItem() { Header = TextBoxx.Text });

        }

        private void NeYt_Click(object sender, RoutedEventArgs e)
        {
            NeAt.Items.Add(new TreeViewItem() { Header = TextBoxx.Text });

        }
    }
}
