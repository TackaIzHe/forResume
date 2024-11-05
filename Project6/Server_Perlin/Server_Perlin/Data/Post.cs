using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Perlin.Data
{
    public class Post
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string aftr { get; set; }
        public string data { get; set; }
        public byte[] img { get; set; }
    }
}
