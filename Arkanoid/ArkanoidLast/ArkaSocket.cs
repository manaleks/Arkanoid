using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace ArkanoidLast
{
    public class ArkaSocket
    {
        public bool isItServ;
        public  int posTOP = 1000;
        public  int posDOWN = 1000;
        public  int ballY;
        public  int ballX;
        public  int blockDel = -1;
        public  bool network = false;
        public  Bitmap scr;

        public ArkaSocket()
        {
            /* Проверка подходящих айпи-адресов
IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
foreach (IPAddress ip in addrs)
    if (ip.AddressFamily == AddressFamily.InterNetwork)
    //    MessageBox.Show(ip.ToString());
*/
        }

        public void TestSEND()
        {
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("10.0.1.27", 8888);
            while (true)
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                }
                //    pictureBox1.Image = (Image)bmp;
                NetworkStream serverStream = clientSocket.GetStream();
                TypeConverter bmpConverter = TypeDescriptor.GetConverter(bmp.GetType());
                byte[] outStream = (byte[])bmpConverter.ConvertTo(bmp, typeof(byte[]));
                serverStream.Write(outStream, 0, outStream.Length);
                bmp.Save("screen.bmp");
                
            }
        }

        public void TestACCEPT()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse("10.0.1.27"), 8888);
            serverSocket.Start();
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new byte[10000000];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                MemoryStream ms = new MemoryStream(bytesFrom);
                Bitmap bitmap1 = (Bitmap)Image.FromStream(ms);
                Program.arkaSocket.scr = bitmap1;
                //   bitmap1.Save("screen.bmp");
                //startPic.Image = (Image)bitmap1;  
            }
        }

        public void NetCheck()
        {

        }

        public void ServerRun()
        {
            isItServ = true;

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string data = null;

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();

                    // Мы дождались клиента, пытающегося с нами соединиться
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Program.arkaSocket.posTOP = Convert.ToInt32(data);

                    // Отправляем ответ клиенту
                    string message = "" + posTOP + " " + ballX + " " + ballY + " " + blockDel;
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                //    msg = Encoding.UTF8.GetBytes(Program.serv.scr.ToString());
                    handler.Send(msg);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                    Program.arkaSocket.network = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Program.arkaSocket.network = false;
            }
        }


        public void ClientSend()
        {
            try
            {
                isItServ = false;
                while (Cursor.Position.X != 0)
                {
                    byte[] bytes = new byte[1024];
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("10.0.1.27"), 11000);
                    Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Соединяем сокет с удаленной точкой
                    sender.Connect(ipEndPoint);
                    string message = Cursor.Position.X.ToString();
                    byte[] msg = Encoding.UTF8.GetBytes(message);

                    // Отправляем данные через сокет
                    int bytesSent = sender.Send(msg);
                    // Получаем ответ от сервера
                    int bytesRec = sender.Receive(bytes);

                    string ansver = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    string[] data = ansver.Split(' ');
                    Program.arkaSocket.posTOP = Convert.ToInt32(data[0]);
                    Program.arkaSocket.ballX = Convert.ToInt32(data[1]);
                    Program.arkaSocket.ballY = Convert.ToInt32(data[2]);
                    Program.arkaSocket.blockDel = Convert.ToInt32(data[3]);
                    Program.arkaSocket.network = true;
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

