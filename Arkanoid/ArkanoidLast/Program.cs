using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static ArkaSocket arkaSocket = new ArkaSocket();
        public static ArkaSocket arkaSocket2 = new ArkaSocket();

        static void DobleRun()
        {
            Thread myThreadServ = new Thread(arkaSocket.ServerRun); //Создаем новый объект потока (Thread)
            myThreadServ.Start(); //запускаем поток

            Thread.Sleep(50);  // немного ждём, чтобы сервер с формой правильно прогрузились

            Thread myThreadClient = new Thread(arkaSocket.ClientSend); //Создаем новый объект потока (Thread)
            myThreadClient.Start(); //запускаем поток
                
            Application.Run(new GameForm());
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //  Thread myThread = new Thread(serv.ServerRun); //Создаем новый объект потока (Thread)
            Thread myThread = new Thread(DobleRun); //Создаем новый объект потока (Thread)
            myThread.Start(); //запускаем поток

            Application.Run(new GameForm());

        }
    }
}
