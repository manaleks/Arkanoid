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
    public partial class Racket : PictureBox
    {
        public int moveSpeed = 10;
        public bool isTheBottom; // нижняя или верхняя ракетка
        public int horizontalAxis; // горизонталь, по которой может двигаться ракетка
        public bool isMovingLeft = false;
        public bool isMovingRight = false;
        GameForm form;

        public Racket(GameForm form, bool isTheBottom)
        {
            InitializeComponent();

            this.isTheBottom = isTheBottom;
            this.form = form;
            this.Size = new Size(64, 18);
            this.SizeMode = PictureBoxSizeMode.StretchImage;

            if (isTheBottom)
                this.horizontalAxis = form.Size.Height - 90;
            else
                this.horizontalAxis = 30;
                
            this.Location = new Point(form.Width / 2 - this.Size.Width / 2, this.horizontalAxis);
            this.Image = new Bitmap(Image.FromFile("Resources/player.png"));
            
        }

        public void moveLeft() // движение ракетки влево
        {
            this.Location = new Point(this.Location.X - this.moveSpeed, this.horizontalAxis);
        }

        public void moveRight()  // движение ракетки вправо
        {
            this.Location = new Point(this.Location.X + this.moveSpeed, this.horizontalAxis);
        }

        public void upgradeSize() // бонус - увеличение размера ракетки
        {
            this.Size = new Size(this.Size.Width + 16, this.Size.Height);
        }
    }
}
