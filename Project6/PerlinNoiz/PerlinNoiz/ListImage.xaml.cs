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
using System.Drawing;
using PerlinNoiz.Data;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Media3D;

namespace PerlinNoiz
{
    /// <summary>
    /// Логика взаимодействия для ListImage.xaml
    /// </summary>
    public partial class ListImage : Window
    {
        DbContext1 db = new DbContext1();
        string NameSelectItem = null;
        BitmapNoiz selectedItem = new BitmapNoiz();
        public ListImage()
        {
            InitializeComponent();
           
        }
        public  List<string> View()
        {
            List<string> result = new List<string>();
            result.Add("Hello_World");
            var a =db.bitmapNoizs.ToList();
            foreach (var item in a) 
                result.Add(item.Name);
            return result;
        }

        private void OpenContecst(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.ToString().Split(':')[1].Split(' ')[1] =="Hello_World")
            {
                image.Source = null;
                NameSelectItem= null;
                textBlock.Visibility= Visibility.Visible;
                return;
            }
            
            string str=e.OriginalSource.ToString().Split(':')[1];
            str = str.Split(' ')[1];
            NameSelectItem=str;
            BitmapImage bitmapImage = new BitmapImage();
            foreach (var item in db.bitmapNoizs.ToList())
            {
                if (item.Name == str)
                {
                    bitmapImage=imageSourse(item.Image);
                    selectedItem = item;
                    break;

                }
            }
            textBlock.Visibility = Visibility.Hidden;
            image.Source = bitmapImage;
           
            
            
            //доделать+ 
        }
        public static BitmapImage imageSourse(byte[] bytes)
        {
            return bytes.byteArrayToBitmap().ToBitmapImage() ;
        }

        public static Bitmap imageSourse(byte[] bytes,int i)
        {
            return bytes.byteArrayToBitmap();
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            if(NameSelectItem== null)
            {
                MessageBox.Show("Выберете элемент");
                return;
            }
            foreach(var item in db.bitmapNoizs.ToList())
            {
                if(item.Name == NameSelectItem)
                {
                    BitmapNoiz i = db.bitmapNoizs.Find(item.Id);
                    db.bitmapNoizs.Remove(i);
                    db.SaveChanges();
                    ListImage1.ItemsSource= View();
                    image.Source = null;
                    MessageBox.Show("Элемент удалён");
                    return;
                }
            }
            
        }

        private void openSaveForm(object sender, RoutedEventArgs e)
        {
            BitmapImage image = null;
            if (NameSelectItem == null)
            {
                MessageBox.Show("Выберете элемент");
                return;
            }
            foreach (var item in db.bitmapNoizs.ToList())
            {
                if (item.Name == NameSelectItem)
                {
                    byte[] i = db.bitmapNoizs.Find(item.Id).Image;
                    image = imageSourse(i);
                                       
                    
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = NameSelectItem;
            saveFileDialog.DefaultExt = ".png";
            saveFileDialog.Filter = "Image (.png)|*.png";
            saveFileDialog.ShowDialog();
            

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
            using (var stream = saveFileDialog.OpenFile())
            {
                encoder.Save(stream);
            }
            MessageBox.Show("Элемент Сохранён");
        }

        private void OpenFullScren(object sender, RoutedEventArgs e)
        {
            Bitmap image = null;
            if (NameSelectItem == null)
            {
                MessageBox.Show("Выберете элемент");
                return;
            }
            foreach (var item in db.bitmapNoizs.ToList())
            {
                if (item.Name == NameSelectItem)
                {
                    byte[] i = db.bitmapNoizs.Find(item.Id).Image;
                    image = imageSourse(i,1);


                }
            }


            new MainWindow().startWinForm(image);
        }

        async private void upLoadImage(object sender, RoutedEventArgs e)
        {

            if(NameSelectItem == null)
            {
                MessageBox.Show("Выбирите изображение");
                return;
            }
            else if (MainWindow.connection.status == true)
            {
                MainWindow.connection.SendPost($"setPost {selectedItem.Name} {selectedItem.Aftr}", selectedItem.Image);
                await Task.WhenAll(MainWindow.connection.endImgT);
                MessageBox.Show("PostUpLoad");
            }
            else
            {
                MessageBox.Show("Вы не подключены к серверу");
            }


        }
    }
}
