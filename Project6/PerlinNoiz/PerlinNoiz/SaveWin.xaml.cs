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
using System.Drawing.Imaging;
using PerlinNoiz.Data;

namespace PerlinNoiz
{
    /// <summary>
    /// Логика взаимодействия для SaveWin.xaml
    /// </summary>
    public partial class SaveWin : Window
    {
        DbContext1 db = new DbContext1();
        public Bitmap bitmap;
        public SaveWin()
        {
            InitializeComponent();
        }

        private void Send(object sender, KeyEventArgs e)
        {
            
            if(e.Key==Key.Enter)
            {
                string newName = name.Text;
                byte[]image= bitmap.ToByteArray(ImageFormat.Bmp);
                if(name.Text.Split(' ').Length>1)
                {
                    char[] a = new char[name.Text.Length];
                    for(int i =0; i<name.Text.Length;i++)
                    {
                        if(name.Text[i] == ' ' || name.Text[i] == ':')
                        {
                            a[i] = '_';
                            continue;
                        }
                        a[i] = name.Text[i];
                    }
                    newName = new string(a);
                }
                db.bitmapNoizs.Add(new BitmapNoiz { Name = newName,Aftr="I", Image = image});
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
