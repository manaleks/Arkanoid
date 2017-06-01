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
    static class Program
    {
        public static int posTOP = 1000;
        public static int posDOWN = 1000;
        public static bool work = true;
        // Назначаем сокет локальной конечной точке и слушаем входящие сокеты


        static void GOOLIKErun()
        {
            Server server = new Server();

            Thread myThread = new Thread(server.ServerRun); //Создаем новый объект потока (Thread)
            myThread.Start(); //запускаем поток

            Application.Run(new GameForm());
            work = false;
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GOOLIKErun();

        }
    }
}
