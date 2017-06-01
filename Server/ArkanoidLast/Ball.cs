using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArkanoidLast
{
    public partial class Ball : PictureBox
    {
        public Point speed = new Point(2, -8);
        public bool launched = false; // был ли запущен мяч с ракетки

        public Ball()
        {
            InitializeComponent();
            this.Size = new Size(16, 16);
            this.Image = new Bitmap(Image.FromFile("Resources/ball.png"), this.Size);
        }

        public void move()
        {
            this.Location = new Point(this.Location.X + this.speed.X, this.Location.Y + this.speed.Y);
        }

        public void flipX() // отражение по горизонтали
        {
            this.speed.X *= -1;
        }

        public void flipY() // отражение по вертикали
        {
            this.speed.Y *= -1;
        }

        public bool collision(PictureBox obj) // реагирование и проверка на столкновения мяча с заданным объектом
        {
            bool collided = false;

            // используем центральные точки границ мяча, чтобы проверить столкновения
            Point topCenter = new Point(this.Location.X + this.Size.Width / 2, this.Location.Y);
            Point bottomCenter = new Point(this.Location.X + this.Size.Width / 2, this.Location.Y + this.Size.Height);
            Point leftCenter = new Point(this.Location.X, this.Location.Y + this.Size.Height / 2);
            Point rightCenter = new Point(this.Location.X + this.Size.Width, this.Location.Y + this.Size.Height / 2);

            // столкновение с левой стороны мяча
            if (leftCenter.Y >= obj.Location.Y &&
                leftCenter.Y <= obj.Location.Y + obj.Size.Height &&
                leftCenter.X >= obj.Location.X &&
                leftCenter.X <= obj.Location.X + obj.Width)
            {
               // если мяч двигался влево до удара => отражаем по горизонтали влево
                if (this.speed.X < 0) this.flipX();
                else this.speed.X = 8;

                collided = true;
            }
            // столкновение с правой стороны мяча
            else if (rightCenter.Y >= obj.Location.Y &&
                rightCenter.Y <= obj.Location.Y + obj.Size.Height &&
                rightCenter.X >= obj.Location.X &&
                rightCenter.X <= obj.Location.X + obj.Width)
            {
                if (this.speed.X > 0) this.flipX();
                else this.speed.X = -8;

                collided = true;
            }
            // столкновение с верхней стороны мяча
            else if (topCenter.X >= obj.Location.X &&
                topCenter.X <= obj.Location.X + obj.Size.Width &&
                topCenter.Y >= obj.Location.Y &&
                topCenter.Y <= obj.Location.Y + obj.Height)
            {
                if (this.speed.Y < 0) this.flipY();
                else this.speed.Y = 8;

                collided = true;

                if (obj is Racket)
                {
                    float surfaceHit = 0;
                    // Определяем в какую часть ракетки (в процентах) попал мяч  и изменяем его направление
                    surfaceHit = (topCenter.X - obj.Location.X) / (float)obj.Size.Width * 100;
                    fromTheRacket(surfaceHit);
                }

            }
            // столкновение с нижней стороны мяча
            else if (bottomCenter.X >= obj.Location.X &&
                bottomCenter.X <= obj.Location.X + obj.Size.Width &&
                bottomCenter.Y >= obj.Location.Y &&
                bottomCenter.Y <= obj.Location.Y + obj.Height)
            {
                if (this.speed.Y > 0) this.flipY();
                else this.speed.Y = -8;

                collided = true;

                // если мяч попадает в ракетку
                if (obj is Racket)
                {
                    float surfaceHit = 0;
                    // Определяем в какую часть ракетки (в процентах) попал мяч  и изменяем его направление
                    surfaceHit = (bottomCenter.X - obj.Location.X) / (float)obj.Size.Width * 100;
                    fromTheRacket(surfaceHit);
                }
            }

            return collided;
        }

        public void fromTheRacket(float surfaceHit)
        {
            if (surfaceHit >= 0 && surfaceHit < 15) this.speed.X = -4;  // Удар слева => мяч после удара летит влево
            else if (surfaceHit >= 15 && surfaceHit < 25) this.speed.X = -3;
            else if (surfaceHit >= 25 && surfaceHit < 35) this.speed.X = -2;
            else if (surfaceHit >= 35 && surfaceHit < 49) this.speed.X = -1;
            else if (surfaceHit >= 49 && surfaceHit <= 51) this.speed.X = 0; // Удар в центр => мяч полетит вверх
            else if (surfaceHit > 51 && surfaceHit <= 65) this.speed.X = 1;
            else if (surfaceHit > 65 && surfaceHit <= 75) this.speed.X = 2;
            else if (surfaceHit > 75 && surfaceHit <= 85) this.speed.X = 3;
            else if (surfaceHit > 85 && surfaceHit <= 100) this.speed.X = 4; // Удар справа => мяч после удара летит вправо
        }
    }
}
