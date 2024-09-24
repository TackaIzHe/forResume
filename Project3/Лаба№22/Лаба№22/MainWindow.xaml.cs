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

namespace Лаба_22
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] amain;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            int[,] a = new int[Convert.ToInt32(N.Text), Convert.ToInt32(M.Text)];
            for(int i = 0; i<Convert.ToInt32(N.Text); i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                space.ColumnDefinitions.Add(col);
            }
            for(int j = 0; j<Convert.ToInt32(M.Text); j++)
            {
                RowDefinition row = new RowDefinition();
                space.RowDefinitions.Add(row);
            }

            for(int i = 0; i<Convert.ToInt32(N.Text); i++)
            {
                for(int j = 0; j<Convert.ToInt32(M.Text); j++)
                {
                    a[i, j] = rand.Next(101);
                    TextBlock tt = new TextBlock();
                    space.Children.Add(tt);
                    tt.SetValue(Grid.ColumnProperty, j);
                    tt.SetValue(Grid.RowProperty, i);
                    tt.Text = a[i, j].ToString();
                }
            }
            amain = a;


        }

        private void Max(object sender, RoutedEventArgs e)
        {
            int[] aMax = new int[Convert.ToInt32(N.Text)];
            int max = 0;
            for (int i = 0; i < Convert.ToInt32(N.Text); i++)
            {
                for(int j =0; j < Convert.ToInt32(M.Text); j++)
                {
                    if (amain[i,j]>=max)
                    {
                        max = amain[i,j];
                    }
                    if(j==Convert.ToInt32(M.Text)-1)
                    {
                        aMax[i] = max;
                        max = 0;
                    }
                }
            }
            
            
            for(int i=0; i<Convert.ToInt32(N.Text); i++)
            {
                RowDefinition row = new RowDefinition();
                space1.RowDefinitions.Add(row);
                TextBlock textBlock = new TextBlock();
                space1.Children.Add(textBlock);
                textBlock.SetValue(Grid.RowProperty, i);
                textBlock.Text = aMax[i].ToString();
            }
        }
    }
}
