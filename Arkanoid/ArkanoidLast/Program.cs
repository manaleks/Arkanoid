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
        /// <summary>
        /// метод для создания и запуска формы с сервером
        /// </summary>
        static void formServRUN()
        {
            Application.Run(new GameForm(true));
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           // new Thread(formServRUN).Start();   // создаём и запускаем форму сервера в отдельном потоке

           // Thread.Sleep(1000);     // ждём уверенную прогрузку сервера

            Application.Run(new GameForm(false));   // создаём и запускаем форму клиента

        }
    }
}

