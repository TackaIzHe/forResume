using PerlinNoiz.Data;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using System.Drawing;

namespace PerlinNoiz
{
    /// <summary>
    /// Логика взаимодействия для ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        private string str = "";
        public ServerWindow()
        {
            InitializeComponent();
            
        }

        private void openImg(object sender, RoutedEventArgs e)
        {
            string s = e.OriginalSource.ToString().Split(':')[2].Split(',')[0];
            s = s.Split(' ')[1];
            str = s;
            
            MainWindow.connection.SendMessage($"getPostImg {s}");
            chek();
            string a=MainWindow.connection.stringb.ToString();
            Console.WriteLine(a);
            List<Comments> comm = new List<Comments>();
            foreach(string i in a.Split(';'))
            {
                if (i.Split(',')[0]==s)
                {
                    Comments comments = new Comments
                    {
                        Id = Convert.ToInt32(i.Split(',')[0]),
                        Aftr = i.Split(',')[1],
                        Date = i.Split(',')[2],
                        Text = i.Split(',')[3]
                    };
                    comm.Add(comments);
                }  
            }

            foreach(Comments comments in comm)
            {
                listcom.Items.Add(comments.Text);
            }

            

        }
        async private void chek()
        {
            while (true)
            {
                await Task.Delay(1000);
                if(MainWindow.connection.img != null )
                {
                    BitmapImage bitmapImage = ListImage.imageSourse(MainWindow.connection.img);
                    this.img.Source = bitmapImage;
                }

            }
        }

        private void addCom(object sender, RoutedEventArgs e)
        {
            MainWindow.connection.SendMessage($"setComm {str} i qwe");
        }
    }
}
