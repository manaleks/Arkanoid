using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Form1 : Form
    {
        public int speed_Left = 4; // скорость шара
        public int speed_Top = 4;
        public int points = 0; // набранные баллы
        public int racket = 0; // номер ракетки, которая последняя отбила мяч

        /// <summary>
        /// Подготовка формы к игре, запуск игры.
        /// </summary>
        public void NewGame()
        {
            ball.Top = 200;
            ball.Left = 50;
            speed_Left = 4;
            speed_Top = 4;
            racket = 0;
            points = 0;
            points_lbl.Text = points.ToString();
            gameover_lbl.Visible = false;
            timer1.Enabled = true;

#if DEBUG
            //// tests
            lblHit.Text = "";
            lbllog.Text = "";
#endif
        }

        /// <summary>
        /// Подготовка формы к работе.
        /// </summary>
        public void InitializeForm()
        {
            // Устанавливаем место меню окончания игры и прячем его
            gameover_lbl.Left = (playground.Width / 2) - (gameover_lbl.Width / 2);
            gameover_lbl.Top = (playground.Height / 2) - (gameover_lbl.Height / 2);
            gameover_lbl.Visible = false;

            Cursor.Hide();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds; // полноэкранный режим (задаем границы по размеру экрана)

            racket1.Top = playground.Bottom - (playground.Bottom / 10); // устанавливаем вертикальную позицию первой ракетки
            racket2.Top = playground.Top + (playground.Bottom / 10); // устанавливаем вертикальную позицию второй ракетки

            NewGame();
        }

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        // Сама игра. Каждый тик программа корректирует положение элементов.
        private void timer1_Tick(object sender, EventArgs e)
        {
            racket1.Left = Cursor.Position.X - (racket1.Width / 2); // устанавливаем центр ракетки 1 на курсор
            racket2.Left = Cursor.Position.X - (racket1.Width / 2); // устанавливаем центр ракетки 2 на курсор

            // Перемещаем мяч на расстояние его скорости
            ball.Left += speed_Left;
            ball.Top += speed_Top;
#if DEBUG
            lbllog.Text = "Координаты верхней ракетки: " + "(" + racket2.Left.ToString() + "; " + racket2.Top.ToString() + ")" +
                        "\n" + "Координаты нижней ракетки: " + "(" + racket1.Left.ToString() + "; " + racket1.Top.ToString() + ")" +
                        "\n" + "Координаты шара: " + "(" + ball.Left.ToString() + "; " + ball.Top.ToString() + ")" +
                        "\n" + "Скорость: " + "(" + speed_Left.ToString() + "; " + speed_Top.ToString() + ")";

            Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = playground.CreateGraphics();
     
            formGraphics.DrawLine(myPen, ball.Left, ball.Top, ball.Left- speed_Left, ball.Top - speed_Top);
            formGraphics.DrawLine(myPen, ball.Right, ball.Top, ball.Right - speed_Left, ball.Top - speed_Top);
            formGraphics.DrawLine(myPen, ball.Left, ball.Bottom, ball.Left - speed_Left, ball.Bottom - speed_Top);
            formGraphics.DrawLine(myPen, ball.Right, ball.Bottom, ball.Right - speed_Left, ball.Bottom - speed_Top);

#endif

            // Столкновение с 1 ракеткой. 
            if (ball.Bottom >= racket1.Top && ball.Bottom <= racket1.Bottom && ball.Right >= racket1.Left && ball.Left <= racket1.Right)
            {
                if (racket != 1)
                {
#if DEBUG
                    lblHit.Text += points + 1 + " ball: " + ball.Location.ToString() + "\n" +
                        "racket1: " + racket1.Location.ToString() + "\n";
#endif
                    speed_Top += 2;
                    speed_Left += 2;

                    //       if (ball.Right > racket1.Left + 100)
                    //          speed_Left = -speed_Left;

                    //      if (ball.Left < racket1.Left + 100)
                    //           speed_Left = -speed_Left;

                    speed_Top = -speed_Top; // изменяем вертикальное направление
                    points += 1;
                    points_lbl.Text = points.ToString();
                }
                racket = 1;
            }

            // Столкновение со 2 ракеткой.
            if (ball.Bottom >= racket2.Bottom && ball.Top <= racket2.Bottom && ball.Right >= racket2.Left && ball.Left <= racket2.Right)
            {
                if (racket != 2)
                {
#if DEBUG
                    lblHit.Text += points + 1 + " ball: " + ball.Location.ToString() + "\n" +
                    "racket2: " + racket2.Location.ToString() + "\n";
#endif
                    speed_Top += 2;
                    speed_Left += 2;

                    speed_Top = -speed_Top; // изменяем вертикальное направление
                    points += 1;
                    points_lbl.Text = points.ToString();

                }
                racket = 2;
            }

            // Попадание шара в боковую стену, горизонтальное направление меняется на противоположное.
            if (ball.Left <= playground.Left || ball.Right >= playground.Right)
            {
                speed_Left = -speed_Left;
            }

            // Вылет шара, конец игры.
            if (ball.Bottom >= playground.Bottom || ball.Top <= playground.Top)
            {
                timer1.Enabled = false;
                gameover_lbl.Visible = true;
            }
        }

        // Обработка нажатий клавиш. Escape – выход. F1 – новая игра.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   // Выход из игры
                this.Close();

            if (e.KeyCode == Keys.F1)   // Новая игра
            {
                NewGame();
            }

            if (e.KeyCode == Keys.A)
            {
                racket2.Left -= 50; 
                points -= 1;
                points_lbl.Text = points.ToString();
            }
            if (e.KeyCode == Keys.D)
            {
                racket2.Left += 50; 
                points += 1;
                points_lbl.Text = points.ToString();
            }

        }

        private void playground_Click(object sender, EventArgs e)
        {    
            if(timer1.Enabled == true)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
        }
    }
}
