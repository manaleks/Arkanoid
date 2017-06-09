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
        public int posTOP = 1000;
        public int posBOTTOM = 1000;
        public int ballY;
        public int ballX;
        public int currentLevel = 1;
        public int clientCursor;
        public int scoreBOTTOM;
        public int scoreTOP;
        public int lifesBOTTOM;
        public int lifesTOP;
        public int widthBOTTOM = 60;
        public int widthTOP = 60;
        public int standCount = 0;
        public string blocksToDel = "";

        public bool work = true;

        public bool network = false;

        public ArkaSocket(bool isItServ)
        {
            this.isItServ = isItServ;

           /*            //  Проверка подходящих айпи-адресов
            IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in addrs)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    MessageBox.Show(ip.ToString());
            */
        }

        public void ServerRun()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Создаем сокет Tcp/Ip
            string clientData = null;

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
                    clientData = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    clientCursor = Convert.ToInt32(clientData);

                    // Отправляем ответ клиенту
                    string message = "" + posTOP + " " + posBOTTOM + " " + ballX + " " +
                        ballY + " " + currentLevel + " " + scoreBOTTOM + " " + scoreTOP + " " + 
                        lifesBOTTOM + " " + lifesTOP + " " + widthBOTTOM + " " + widthTOP + " " + blocksToDel;
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    //  msg = Encoding.UTF8.GetBytes(scr.ToString());
                    handler.Send(msg);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    network = true;
                }
            }
            catch (SocketException ex)
            {
                // MessageBox.Show(ex.ToString());
                MessageBox.Show("Ошибка соединения, возможно, порт уже занят другим сервисом.");
            }
            finally
            {
                network = false;
            }
        }


        public void ClientRun()
        {
            try
            {
                while (work)
                {
                    byte[] bytes = new byte[1024];
                    //           IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("10.43.23.13"), 11000);
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
                    Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Соединяем сокет с удаленной точкой
                    sender.Connect(ipEndPoint);
                    string message = clientCursor.ToString();
                    byte[] msg = Encoding.UTF8.GetBytes(message);

                    // Отправляем данные через сокет
                    int bytesSent = sender.Send(msg);
                    // Получаем ответ от сервера
                    int bytesRec = sender.Receive(bytes);

                    string ansver = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    string[] data = ansver.Split(' ');
                    posTOP = Convert.ToInt32(data[0]);
                    posBOTTOM = Convert.ToInt32(data[1]);
                    ballX = Convert.ToInt32(data[2]);
                    ballY = Convert.ToInt32(data[3]);
                    currentLevel = Convert.ToInt32(data[4]);
                    scoreBOTTOM = Convert.ToInt32(data[5]);
                    scoreTOP = Convert.ToInt32(data[6]);
                    lifesBOTTOM = Convert.ToInt32(data[7]);
                    lifesTOP = Convert.ToInt32(data[8]);
                    widthBOTTOM = Convert.ToInt32(data[9]);
                    widthTOP = Convert.ToInt32(data[10]);
                    blocksToDel = data[11];

                    network = true;
                }
            }
            catch (SocketException)
            {
              //  MessageBox.Show(ex.ToString());
                isItServ = true;
                ServerRun();
            }
            finally
            {
                network = false;
            }
        }
    }
}


