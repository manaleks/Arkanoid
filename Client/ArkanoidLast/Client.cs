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
        int clientPOS = 1000;

        public Client()
        {
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
            finally
            {
                MessageBox.Show("Потеря сети");
            }
        }

        void SendMessageFromSocket(int port)
        {
            IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in addrs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    while (Cursor.Position.X != 0 && Program.work == true)
                    {
                        // Устанавливаем удаленную точку для сокета
                        byte[] bytes = new byte[1024];
                        // Устанавливаем удаленную точку для сокета

                        IPEndPoint ipEndPoint = new IPEndPoint(ip, 11001);

                        // IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("10.0.1.27"), 11001);
                        Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        // Соединяем сокет с удаленной точкой
                        sender.Connect(ipEndPoint);
                        string message = Cursor.Position.X.ToString();
                        byte[] msg = Encoding.UTF8.GetBytes(message);

                        // Отправляем данные через сокет
                        int bytesSent = sender.Send(msg);
                        // Получаем ответ от сервера
                        int bytesRec = sender.Receive(bytes);

                        clientPOS = Convert.ToInt32(Encoding.UTF8.GetString(bytes, 0, bytesRec));

                        Program.posDOWN = clientPOS;
                    }
                }
            }
        }
    }
}

