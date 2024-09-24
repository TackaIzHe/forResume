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

namespace win2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int size_Pen = 2; 
        public Color a = Colors.Black;
        public MainWindow()
        {
            InitializeComponent();
            
        }



        private void Update(object sender, MouseButtonEventArgs e)
        {           
            int size = Convert.ToInt16(sliderSize.Value);
            size_date.Text = size.ToString();
            sliderSize.Value = size;
            size_Pen = size;

        }
        
        private void Update1(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                size_Pen = Convert.ToInt16(size_date.Text);
            sliderSize.Value = size_Pen;
        }













        private void White (object sender, RoutedEventArgs e)
        {
            a = Colors.White;
            
        }

        private void Black(object sender, RoutedEventArgs e)
        {
            a = Colors.Black;

        }

        private void Red(object sender, RoutedEventArgs e)
        {
            a = Colors.Red;

        }

        private void Yellow(object sender, RoutedEventArgs e)
        {
            a = Colors.Yellow;

        }

        private void Green(object sender, RoutedEventArgs e)
        {
            a = Colors.Green;

        }

        private void Blue(object sender, RoutedEventArgs e)
        {
            a = Colors.Blue;

        }

        private void Purple(object sender, RoutedEventArgs e)
        {
            a = Colors.Purple;

        }

        private void Gray(object sender, RoutedEventArgs e)
        {
            a = Colors.Gray;

        }

        private void Pink(object sender, RoutedEventArgs e)
        {
            a = Colors.Pink;

        }

        private void Orange(object sender, RoutedEventArgs e)
        {
            a = Colors.Orange;

        }

        private void Aqua(object sender, RoutedEventArgs e)
        {
            a = Colors.Aqua;

        }

        private void Azure(object sender, RoutedEventArgs e)
        {
            a = Colors.Azure;

        }

        private void Brow(object sender, RoutedEventArgs e)
        {
            a = Colors.Brown;

        }

        private void DarkBlue(object sender, RoutedEventArgs e)
        {
            a = Colors.DarkBlue;

        }

    }
}
