using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;


namespace ЛАБА__20
{

    public partial class MainWindow : System.Windows.Window
    {
        win2.MainWindow win = new win2.MainWindow();

        public MainWindow()
        {
            InitializeComponent();
            InkCanvas1.UseCustomCursor = true;
            InkCanvas1.Cursor = Cursors.Pen;
        }      

        private void qwe(object sender, RoutedEventArgs e)
        {
            
            win = new win2.MainWindow();
            win.Owner=this;
            win.Show();
            Update();
        }
        private void qwe1(object sender, MouseButtonEventArgs e)
        {

            win = new win2.MainWindow();
            win.Owner = this;
            win.Show();
            Update();
        }

        async public void Update()
        {
            bool a = true;
            while (a)
            {
                await Task.Delay(150);
                color.Color = win.a;
                color.Width = win.size_Pen;
                color.Height = win.size_Pen;
            }
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "savedimage"; // Default file name
            dlg.DefaultExt = ".jpg"; // Default file extension
            dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                //get the dimensions of the ink control
                int margin = (int)this.InkCanvas1.Margin.Left;
                int width = (int)this.InkCanvas1.ActualWidth - margin;
                int height = (int)this.InkCanvas1.ActualHeight - margin;
                //render ink to bitmap
                RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(InkCanvas1);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
            }
        }

        private void New_Button(object sender, RoutedEventArgs e)
        {
            this.InkCanvas1.Strokes.Clear();
            this.InkCanvas1.Children.Clear();
        }

        private void Open_Button(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            System.Windows.Controls.Image a = new System.Windows.Controls.Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(dlg.FileName);
            bitmapImage.EndInit();
            a.Source = bitmapImage;
            InkCanvas1.Children.Add(a);
            
        }


        private void New_File_action(object sender, MouseButtonEventArgs e)
        {
            this.InkCanvas1.Strokes.Clear();
            this.InkCanvas1.Children.Clear();
        }

        private void OpenFileAction(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            System.Windows.Controls.Image a = new System.Windows.Controls.Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(dlg.FileName);
            bitmapImage.EndInit();
            a.Source = bitmapImage;
            InkCanvas1.Children.Add(a);
        }
    }
}
