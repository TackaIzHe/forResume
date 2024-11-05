using Microsoft.EntityFrameworkCore.Internal;
using Server_Perlin.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;
using System.Runtime.InteropServices.ComTypes;

namespace MultiServer
{
    public static class BitmapFunc
    {
        public static Bitmap byteArrayToBitmap(this byte[] bytes)
        {
            ImageConverter ic = new ImageConverter();
            return (Bitmap)ic.ConvertFrom(bytes);
        }
    }
    
    class Program
    {
        static DataBaseContext context =new DataBaseContext();
        private static bool status = true;
        private static List<TcpClient> tcpClient = new List<TcpClient>();
        private static TcpListener serverSocket = null;
        private const int BUFFER_SIZE = 16384;
        private const int PORT = 8080;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        static MemoryStream ms;
        private static Action action;
        private static Action action1;
        private static Task end;
        private static Task end1;
        static void Main()
        {
            action = () => endT();
            action1 = () => endT();
            
            
            Console.Title = "Server";
            SetupServer();
            Loopchek();
            LoopConnect();
            LoopGet();
            
            command(Console.ReadLine());
        }


        private static Task endT()
        {
            return Task.Delay(100);
        }

        async private static void Loopchek()
        {
            while (true)
            {
                await Task.Delay(1000);
                try
                {

                    foreach (TcpClient client in tcpClient)
                    {

                        if (client.Connected == false)
                        {
                            Console.WriteLine($"Disconnected: {client.Client.RemoteEndPoint}");
                            client.Close();
                            tcpClient.Remove(client);
                        }
                    }
                }
                catch (Exception ex) 
                { 
                    //Console.WriteLine(ex.ToString()); 
                }
                
                
            }
        }
        static void clientWantGetComm(string str, byte[] buffer,NetworkStream stream)
        {                
                foreach (comment a in context.comments)
                {
                buffer = Encoding.ASCII.GetBytes( $";{a.postId.ToString()},{a.aftr},{a.data},{a.text};");
                stream.Write(buffer, 0, buffer.Length);
                }
            buffer = Encoding.ASCII.GetBytes("endSend");
            stream.Write(buffer,0, buffer.Length);
        }
        static void clientWantGetPost(string str, byte[] buffer, NetworkStream stream)
        {
            buffer = Encoding.ASCII.GetBytes("sendPost");
            stream.Write(buffer, 0, buffer.Length);
            
            int i = stream.Read(buffer, 0, buffer.Length);
            StringBuilder sb = new StringBuilder();
            while(i>0)
            {
                sb.Append(Encoding.ASCII.GetString(buffer));
                
                if(sb.ToString().Contains("canSend"))
                {
                    Console.WriteLine(sb.ToString());
                    sb = null;
                    break;
                }
                i = stream.Read(buffer, 0, buffer.Length);
            }
            foreach(Post a in context.posts)
            {
                string s = $";{a.id},{a.name},{a.aftr},{a.data};";
                buffer = Encoding.ASCII.GetBytes(s);
                stream.Write(buffer,0, buffer.Length);
                Console.WriteLine(s);
                
            }
            buffer = Encoding.ASCII.GetBytes(";;;endSend;;;");
            stream.Write(buffer,0,buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer));

            clientWantGetComm(str,buffer, stream);

            end = new Task(action);
            end.Start();
            
           
        }
        static void clientWantSetComm(string str)
        {
            Console.WriteLine(str);
            comment comment = new comment 
            { 
                postId = Convert.ToInt32(str.Split(' ')[1]), 
                aftr = str.Split(' ')[2], 
                text = str.Split(' ')[3], 
                data = DateTime.Now.ToString() 
            };
            Console.WriteLine(comment.postId);
            
            context.comments.Add(comment);
            context.SaveChanges();

        }

        static void clientWantSetPost(string str)
        {
            

            Post post = new Post
            {
                name = str.Split(' ')[1],
                aftr = str.Split(' ')[2],
                data = DateTime.Now.ToString(),
                img = ms.ToArray()
            };

            context.posts.Add(post);
            context.SaveChanges();
            

        }

        private static void clientWantGetImg(string s, byte[] buffer, NetworkStream stream)
        {
            buffer = Encoding.ASCII.GetBytes("startImg");
            stream.Write(buffer, 0, buffer.Length);
            StringBuilder sb = new StringBuilder();
            int i = stream.Read(buffer, 0, buffer.Length);
            
            while(i>0)
            {
                sb.Append(Encoding.ASCII.GetString(buffer));
                Console.WriteLine(sb.ToString());
                if(sb.ToString().Contains("startImg"))
                {
                    sb.Clear();
                    break;
                }
                i = stream.Read(buffer, 0, buffer.Length);
            }
                    
            MemoryStream ms = new MemoryStream();
            Image a = null;
            foreach (Post img in context.posts)
            {
                bool q = img.id.Equals(Convert.ToInt32(s.Split(' ')[1]));
                if(q)
                {

                    a = img.img.byteArrayToBitmap();
                    break;
                }
            }
            a.Save(ms,ImageFormat.Jpeg);
            buffer = ms.ToArray();
            stream.Write(buffer,0,buffer.Length);

            buffer = Encoding.ASCII.GetBytes(";;;endSend;;;");
            stream.Write(buffer, 0, buffer.Length);
            end1 = new Task(action1);
            end1.Start();
        }

        private static void command(string com)
        {
            while (status)
            {
                if (com == "stop")
                {
                    status = false;
                }
                else if (com=="list con")
                {
                    if (tcpClient.Count == 0)
                    {
                        Console.WriteLine("нет подключений");
                        command(Console.ReadLine());
                    }
                    foreach(TcpClient client in tcpClient)
                    Console.WriteLine(client.Client.RemoteEndPoint);
                    command(Console.ReadLine());

                }
                else
                {
                    command(Console.ReadLine());
                }
            }
        }



        async private static void LoopGet()
        {
            while (true)
            {
                await Task.Delay(1);
                try
                {

                    
                    byte[] buffer = new byte[BUFFER_SIZE];
                    foreach (TcpClient i in tcpClient)
                    {
                        if(i.Connected ==true)
                        {
                            NetworkStream stream = i.GetStream();
                        
                            int ii = stream.Read(buffer, 0, BUFFER_SIZE);
                            StringBuilder sb1 = new StringBuilder();
                            string str =  Encoding.ASCII.GetString(buffer,0,ii);

                            while (ii > 0)
                            {
                                sb1.Append(Encoding.ASCII.GetString(buffer, 0, ii));
                                Console.WriteLine(sb1.ToString());
                                if (sb1.ToString().Split(' ')[0] == "getPostImg")
                                {
                                    
                                    clientWantGetImg(sb1.ToString(), buffer, stream);
                                    await Task.WhenAll(end1);
                                    sb1.Clear();
                                }
                                if (sb1.ToString().Split(' ')[0] == "getComm")
                                {
                                    clientWantGetComm(str, buffer, stream);
                                    sb1.Clear();
                                }
                                if (sb1.ToString().Split(' ')[0] == "getPost")
                                {
                                    clientWantGetPost(str, buffer, stream);
                                    await Task.WhenAll(end);
                                    Console.WriteLine("savePost");
                                    sb1.Clear();

                                }
                                if (sb1.ToString().Split(' ')[0] == "setComm")
                                {
                                    
                                    clientWantSetComm(sb1.ToString());
                                    sb1.Clear();
                                }
                                if (sb1.ToString().Split(' ')[0] == "setPost")
                                {
                                    ms = new MemoryStream();

                                    byte[] mes = Encoding.ASCII.GetBytes("can send img");
                                    stream.Write(mes, 0, mes.Length);

                                    

                                    StringBuilder sb = new StringBuilder();
                                    while (ii > 0)
                                    {
                                        ii = stream.Read(buffer, 0, BUFFER_SIZE);
                                        
                                        ms.Write(buffer, 0, ii);
                                        sb.Append(Encoding.ASCII.GetString(buffer));
                                        if (sb.ToString().Contains(";;;endSend;;;"))
                                        {
                                            buffer = Encoding.ASCII.GetBytes("qqq");
                                            stream.Write(buffer, 0, buffer.Length);
                                            break;
                                        }

                                    }
                                    clientWantSetPost(sb1.ToString());
                                    sb1.Clear();


                                }
                                ii = stream.Read(buffer, 0, buffer.Length);
                            }
                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                    
                }
            }
        }

        

        async private static void LoopConnect()
        {
            while (true)
            {
                try
                {

                    await Task.Delay(1000);
                    TcpClient client = serverSocket.AcceptTcpClient();
                    tcpClient.Add(client);
                    NetworkStream networkStream = client.GetStream();
                    Console.WriteLine($"connected: {client.Client.RemoteEndPoint}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.Clear();
                }

            }
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            IPAddress ipArres = IPAddress.Parse("192.168.0.11");
            serverSocket = new TcpListener(ipArres, PORT);
            serverSocket.Start();
            Console.WriteLine("server started");
        }


    }
}
