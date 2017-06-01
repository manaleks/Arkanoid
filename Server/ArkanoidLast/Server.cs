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
        int serverPOS = 1000;

        public Server()
        {
        }

        public void ServerRun()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11001);
            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                    
                // Начинаем слушать соединения
                while (Cursor.Position.X != 0 && Program.work == true)
                {
                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];

                    int bytesRec = handler.Receive(bytes);

                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    serverPOS = Convert.ToInt32(data);
                    Program.posTOP = serverPOS;

                    // Отправляем ответ клиенту
                    string message = Cursor.Position.X.ToString();
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    handler.Send(msg);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
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
    }
}

