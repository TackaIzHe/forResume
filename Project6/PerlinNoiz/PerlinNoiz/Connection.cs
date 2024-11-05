using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PerlinNoiz.Data;
using System.Windows.Documents;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;


namespace PerlinNoiz
{
    public class Connection
    {
        public bool status = true;
        public bool serverStatus = false;
        public NetworkStream stream;
        public static TcpClient client = new TcpClient();
        static byte[] bytes = new byte[4096];
        public StringBuilder mess = new StringBuilder();
        public byte[] img;
        private static Action actionEndGet;
        private static Action actionEndImg;
        public Task endImgT;
        public Task endGetT;
        public static List<byte[]> imgList = new List<byte[]>();
        public StringBuilder stringb = new StringBuilder();

        public void main()
        {
            LoopCek();
            GetMessage();
            actionEndGet = () => endAction();
            actionEndImg = () => endImg();
            endGetT = new Task(actionEndGet);
            endImgT = new Task(actionEndImg);
            

        }
        private Task endImg()
        {
            
            return Task.Delay(1);
           
        }
        private Task endAction()
        {
            return Task.Delay(1);
        }


        async public void LoopCek()
        {
            await Task.Delay(1000);
            while (status)
            {
                try
                {  
                    if (client.Connected == false)
                    {
                        ConnectToServer();
                        serverStatus = true;
                            
                    }
                }
                catch
                {
                    Console.WriteLine(111);
                }
            }
        }

        public void SendMessage(string message)
        {

            while (status)
            {
                byte[] msg = Encoding.ASCII.GetBytes(message);
                stream.Write(msg, 0, msg.Length);
                break;
            }
        }

        public void SendPost(string message, byte[] img1)
        {
            Image a = ListImage.imageSourse(img1, 1);
            MemoryStream ms = new MemoryStream();
            a.Save(ms,ImageFormat.Jpeg);
            img1 = ms.ToArray();
            img = img1;
            while (status)
            {  
                byte[] msg = Encoding.ASCII.GetBytes(message);
              
                stream.Write(msg, 0, msg.Length);

                break;
            }
        }


        async private void GetMessage()
        {        
            while (status)
            {
                await Task.Delay(2000);

                try
                {
                    if (client.Connected == true)
                    {
                        byte[] buffer = new byte[16384];
                            
                        int i = stream.Read(buffer, 0, buffer.Length);
                                
                        string messs = (Encoding.ASCII.GetString(buffer, 0, i));
                        StringBuilder sb = new StringBuilder();
                        while (i > 0)
                        {
                            sb.Append(Encoding.ASCII.GetString(buffer));
                            if (sb.ToString().Contains("startImg"))
                            {
                               
                                buffer = Encoding.ASCII.GetBytes("startImg");
                                stream.Write(buffer, 0, buffer.Length);
                                i = stream.Read(buffer, 0, buffer.Length);
                                sb.Clear();
                                MemoryStream ms = new MemoryStream();
                                
                                while (i > 0)
                                {
                                    ms.Write(buffer, 0, i);
                                    sb.Append(Encoding.ASCII.GetString(buffer));
                                        
                                    
                                    if(sb.ToString().Contains(";;;endSend;;;"))
                                    {
                                        img = ms.ToArray();
                                        sb.Clear();
                                        break;
                                    }
                                    
                                    i = stream.Read(buffer, 0, buffer.Length);

                                }
                                

                            }
                            if (sb.ToString().Contains("can send img"))
                            {
                                Console.WriteLine(1);
                                stream.Write(img, 0, img.Length);
                                buffer = Encoding.ASCII.GetBytes(";;;endSend;;;");
                                stream.Write(buffer, 0, buffer.Length);
                                mess.Clear();
                                stream.Read(buffer, 0, buffer.Length);
                                endImgT.Start();
                                endImgT = new Task(actionEndImg);
                                sb.Clear();
                                continue;
                            }
                                    
                            if(sb.ToString().Contains("sendPost"))
                            {
                                buffer = Encoding.ASCII.GetBytes("canSend");
                                stream.Write(buffer, 0, buffer.Length);
                                i = stream.Read(buffer, 0, buffer.Length);
                                while (i > 0)
                                {
                                    mess.Append(Encoding.ASCII.GetString(buffer, 0, i));
                                    if (mess.ToString().Contains(";;;endSend;;;"))
                                    {
                                    

                                        endGetT.Start();
                                        endGetT = new Task(actionEndGet);
                                        while(i > 0)
                                        {
                                            i = stream.Read(buffer, 0, buffer.Length);
                                            stringb.Append(Encoding.ASCII.GetString(buffer));
                                            if(stringb.ToString().Contains("endSend"))
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    i = stream.Read(buffer, 0, buffer.Length);


                                }
                                sb.Clear();

                            }
                            
                            i = stream.Read(buffer, 0, buffer.Length);
                        }
                            

                    }



                }
                catch (Exception ex)
                {
                    serverStatus = false;

                    MessageBox.Show("Сервер прекратил свою работу");
                    Console.WriteLine("Сервер прекратил свою работу");
                }
            }    
        }

        public void ConnectToServer()
        {
            while (client == null || client.Connected == false)
            {
                try
                {
                    IPAddress ipAddr = IPAddress.Parse("192.168.0.11");
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8080);

                    TcpClient sender = new TcpClient();
                    sender.Connect(ipEndPoint);
                    client = sender;
                    stream = client.GetStream();
                    MessageBox.Show("Вы подключились");
                    Console.WriteLine("Вы подключились");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Попытка подключения");
                    
                    Console.WriteLine("Попытка подключения");
                }
            }
        }
    }
}
