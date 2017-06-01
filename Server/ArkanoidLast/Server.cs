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
    public class Server
    {
        public Server()
        {
                        /* Проверка подходящих айпи-адресов
            IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in addrs)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                //    MessageBox.Show(ip.ToString());
            */
        }

        public void ServerRun()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11001);
            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string data = null;

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (Cursor.Position.X != 0)
                {
                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    
                    // Мы дождались клиента, пытающегося с нами соединиться
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Program.posTOP = Convert.ToInt32(data);

                    // Отправляем ответ клиенту
                    string message = Cursor.Position.X.ToString();
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    handler.Send(msg);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                    Program.network = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

