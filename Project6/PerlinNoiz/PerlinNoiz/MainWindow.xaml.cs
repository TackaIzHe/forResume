using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Interop;
using PerlinNoiz.Data;
using System.Collections;
using System.Threading;
using System.Xml.Linq;

namespace PerlinNoiz
{


    public partial class MainWindow : Window
    {
        List<Comments> comments = new List<Comments>();
        List<BitmapNoiz> bitmapNoizs = new List<BitmapNoiz>();
        public static Connection connection = new Connection();
        Thread threadServer = null;
        


        public MainWindow()
        {
            InitializeComponent();
            
        }


        public void startWinForm(Bitmap bitmap)
        {
            form1 a = new form1(bitmap);
            PictureBox pictureBox1 = new PictureBox();

            a.SuspendLayout();
            pictureBox1.Name = "image";
            // 
            // Form1
            // 
            a.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            a.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            a.ClientSize = new System.Drawing.Size(1080, 1920);
            a.StartPosition = FormStartPosition.CenterScreen;
            a.Name = "Form1";
            a.Text = "Perlin";
            a.ResumeLayout(false);
            
            pictureBox1.Image = bitmap;
            pictureBox1.Width = 1080;
            pictureBox1.Height = 1920;
            a.Controls.Add(pictureBox1);

            a.ShowDialog();
        }



        private void CreateFirstPerlinImage(object sender, RoutedEventArgs e)
        {
            PerlinNoise perlin = new PerlinNoise(Seed.Text);
            Bitmap bitmap = perlin.FirstPerlinCreate(Mash.Text);
            startWinForm(bitmap);


        }

        private void CreateSecondPerlinImage(object sender, RoutedEventArgs e)
        {
            SecondPerlin perlin = new SecondPerlin();
            Bitmap bitmap = perlin.SecondPerlinCreate(Seed.Text,Mash.Text);
            startWinForm(bitmap);
            
        }
        

        private void OpenListImage(object sender, RoutedEventArgs e)
        {
            ListImage a = new ListImage();
            a.ListImage1.ItemsSource = a.View();
            a.Owner = this;
            a.Show();
            
            
        }

        private void connect(object sender, RoutedEventArgs e)
        {
            if(threadServer == null)
            {
                threadServer = new Thread(new ThreadStart(connection.main));
                threadServer.Start();
                
            }
        }



        private void isClosed(object sender, EventArgs e)
        {
            connection.status = false;
        }

        private List<Comments> ConvertStringCommToObject()
        {

            foreach (string a in connection.mess.ToString().Split(';'))
            {
                try
                {
                    Convert.ToInt32(a.Split(',')[0]);
                    comments.Add(new Comments
                    {
                        Id = Convert.ToInt32(a.Split(',')[0]),
                        Aftr = a.Split(',')[1],
                        Date = a.Split(',')[2],
                        Text = a.Split(',')[3]
                    });
                }
                catch
                {
                    continue;
                }
            }
            connection.mess.Clear();
            return comments;
        }

        private List<BitmapNoiz> ConvertStringBitMapToObject()
        {
            bitmapNoizs.Clear();
            
            foreach (string a in connection.mess.ToString().Split(';'))
            {
                
                try
                {
                    Convert.ToInt32(a.Split(',')[0]);

                    bitmapNoizs.Add(new BitmapNoiz
                    {
                        Id = Convert.ToInt32(a.Split(',')[0]),
                        Name = a.Split(',')[1],
                        Aftr = a.Split(',')[2],
                        Date = a.Split(',')[3],
                        //Image =
                    });
                }
                catch
                {
                    Console.WriteLine(a);
                    continue;
                }
            }

            
            return bitmapNoizs;
        }

        async private void openServerWindow(object sender, RoutedEventArgs e)
        {

            Task tasks = connection.endGetT;
            if (connection.serverStatus == true)
            {
                connection.SendMessage("getPost");
                
                await Task.WhenAll(tasks);
                await Task.Delay(1000);
                bitmapNoizs = ConvertStringBitMapToObject();
                connection.mess.Clear();
                ServerWindow serverWindow = new ServerWindow();
              
                foreach(BitmapNoiz post in bitmapNoizs)
                {
                    serverWindow.list.Items.Add
                        ($"Id: {post.Id}, Name: {post.Name}, Aftr: {post.Aftr}, Date: {post.Date}");
                }

                //serverWindow.img.Source = imageSourse(connection.img);

                serverWindow.Show();
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Вы не подключены");
            }
        }

        BitmapImage imageSourse(byte[] bytes)
        {
            return bytes.byteArrayToBitmap().ToBitmapImage();
        }
    }
}

   


   




    


