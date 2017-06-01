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
    public partial class Block : PictureBox
    {
        public char type; // тип блока

        public Block(char blockType)
        {
            InitializeComponent();

            this.type = blockType;
            this.Size = new Size(48, 18);
            this.Image = new Bitmap(Image.FromFile("Resources/block_" + blockType + ".png"), this.Size);
        }
    }
}
