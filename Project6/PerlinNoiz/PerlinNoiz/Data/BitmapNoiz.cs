using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerlinNoiz.Data
{
    public class BitmapNoiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Aftr { get; set; }
        public string Date { get; set; }
        public byte[] Image { get; set; }

    }
}
