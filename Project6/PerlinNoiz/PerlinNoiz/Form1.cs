using PerlinNoiz.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerlinNoiz
{
    public partial class form1 : Form
    {
        Bitmap image;
        public form1(Bitmap bitmap)
        {
            InitializeComponent();
            image= bitmap;
            
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {

            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("Хотите Сохранить Изображение?");

            MessageBoxButtons button = MessageBoxButtons.YesNo;
            var res=MessageBox.Show(messageBoxCS.ToString(), "FormClosing Event",button);
            if(res==DialogResult.Yes)
            {
                SaveWin a = new SaveWin();
                a.bitmap = image;
                a.ShowDialog();
            }
            if (res == DialogResult.No)
            {
                
            }
        }

        private void Click_Close(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
