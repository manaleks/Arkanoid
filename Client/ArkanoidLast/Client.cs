using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArkanoidLast
{
    public class Client
    {
        public Client()
        {
                        /* Проверка подходящих айпи-адресов
            IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in addrs)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                //    MessageBox.Show(ip.ToString());
            */
        }

        public void ClientRun()
        {
            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void SendMessageFromSocket(int port)
        {
            while (Cursor.Position.X != 0)
                    {
                        byte[] bytes = new byte[1024];
                        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("10.0.1.27"), 11001);
                        Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        // Соединяем сокет с удаленной точкой
                        sender.Connect(ipEndPoint);
                        string message = Cursor.Position.X.ToString();
                        byte[] msg = Encoding.UTF8.GetBytes(message);

                        // Отправляем данные через сокет
                        int bytesSent = sender.Send(msg);
                        // Получаем ответ от сервера
                        int bytesRec = sender.Receive(bytes);

                        Program.posDOWN = Convert.ToInt32(Encoding.UTF8.GetString(bytes, 0, bytesRec));
                Program.network = true;
            }
        }
    }
}

