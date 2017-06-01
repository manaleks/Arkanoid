namespace Arkanoid
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.playground = new System.Windows.Forms.Panel();
            this.ball = new System.Windows.Forms.PictureBox();
            this.racket1 = new System.Windows.Forms.PictureBox();
            this.racket2 = new System.Windows.Forms.PictureBox();
            this.lblHit = new System.Windows.Forms.Label();
            this.lbllog = new System.Windows.Forms.Label();
            this.gameover_lbl = new System.Windows.Forms.Label();
            this.points_lbl = new System.Windows.Forms.Label();
            this.score_lbl = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.playground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.racket1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.racket2)).BeginInit();
            this.SuspendLayout();
            // 
            // playground
            // 
            this.playground.Controls.Add(this.ball);
            this.playground.Controls.Add(this.racket1);
            this.playground.Controls.Add(this.racket2);
            this.playground.Controls.Add(this.lblHit);
            this.playground.Controls.Add(this.lbllog);
            this.playground.Controls.Add(this.gameover_lbl);
            this.playground.Controls.Add(this.points_lbl);
            this.playground.Controls.Add(this.score_lbl);
            this.playground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playground.Location = new System.Drawing.Point(0, 0);
            this.playground.Margin = new System.Windows.Forms.Padding(4);
            this.playground.Name = "playground";
            this.playground.Size = new System.Drawing.Size(737, 528);
            this.playground.TabIndex = 0;
            this.playground.Click += new System.EventHandler(this.playground_Click);
            // 
            // ball
            // 
            this.ball.BackColor = System.Drawing.Color.Red;
            this.ball.Location = new System.Drawing.Point(72, 95);
            this.ball.Margin = new System.Windows.Forms.Padding(4);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(40, 37);
            this.ball.TabIndex = 1;
            this.ball.TabStop = false;
            // 
            // racket1
            // 
            this.racket1.BackColor = System.Drawing.Color.Black;
            this.racket1.Location = new System.Drawing.Point(283, 489);
            this.racket1.Margin = new System.Windows.Forms.Padding(4);
            this.racket1.Name = "racket1";
            this.racket1.Size = new System.Drawing.Size(270, 25);
            this.racket1.TabIndex = 0;
            this.racket1.TabStop = false;
            // 
            // racket2
            // 
            this.racket2.BackColor = System.Drawing.Color.Black;
            this.racket2.Location = new System.Drawing.Point(283, 15);
            this.racket2.Margin = new System.Windows.Forms.Padding(4);
            this.racket2.Name = "racket2";
            this.racket2.Size = new System.Drawing.Size(270, 25);
            this.racket2.TabIndex = 5;
            this.racket2.TabStop = false;
            // 
            // lblHit
            // 
            this.lblHit.AutoSize = true;
            this.lblHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHit.Location = new System.Drawing.Point(27, 427);
            this.lblHit.Name = "lblHit";
            this.lblHit.Size = new System.Drawing.Size(0, 29);
            this.lblHit.TabIndex = 7;
            // 
            // lbllog
            // 
            this.lbllog.AutoSize = true;
            this.lbllog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbllog.Location = new System.Drawing.Point(27, 161);
            this.lbllog.Name = "lbllog";
            this.lbllog.Size = new System.Drawing.Size(0, 29);
            this.lbllog.TabIndex = 6;
            // 
            // gameover_lbl
            // 
            this.gameover_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameover_lbl.AutoSize = true;
            this.gameover_lbl.BackColor = System.Drawing.Color.Transparent;
            this.gameover_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gameover_lbl.Location = new System.Drawing.Point(180, 145);
            this.gameover_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameover_lbl.Name = "gameover_lbl";
            this.gameover_lbl.Size = new System.Drawing.Size(345, 276);
            this.gameover_lbl.TabIndex = 4;
            this.gameover_lbl.Text = "Game Over\r\n\r\nF1 - Restart\r\nEsc - Exit";
            this.gameover_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // points_lbl
            // 
            this.points_lbl.AutoSize = true;
            this.points_lbl.BackColor = System.Drawing.Color.Transparent;
            this.points_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.points_lbl.Location = new System.Drawing.Point(229, 11);
            this.points_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.points_lbl.Name = "points_lbl";
            this.points_lbl.Size = new System.Drawing.Size(63, 69);
            this.points_lbl.TabIndex = 3;
            this.points_lbl.Text = "0";
            // 
            // score_lbl
            // 
            this.score_lbl.AutoSize = true;
            this.score_lbl.BackColor = System.Drawing.Color.Transparent;
            this.score_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.score_lbl.Location = new System.Drawing.Point(4, 11);
            this.score_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.score_lbl.Name = "score_lbl";
            this.score_lbl.Size = new System.Drawing.Size(203, 69);
            this.score_lbl.TabIndex = 2;
            this.score_lbl.Text = "Score:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 528);
            this.Controls.Add(this.playground);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.playground.ResumeLayout(false);
            this.playground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.racket1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.racket2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel playground;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox racket1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label points_lbl;
        private System.Windows.Forms.Label score_lbl;
        private System.Windows.Forms.Label gameover_lbl;
        private System.Windows.Forms.PictureBox racket2;
        private System.Windows.Forms.Label lblHit;
        private System.Windows.Forms.Label lbllog;
    }
}

